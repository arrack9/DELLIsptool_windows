using System;
using System.Runtime.InteropServices;
using System.Threading;


public class MStarISP
{
    #region Windows API

    [DllImport("USBtoI2C.dll", CharSet = CharSet.Auto)]
    private static extern IntPtr OpenDeviceWithID([In] System.UInt16 VendorID, [In] System.UInt16 ProductID, [In] System.UInt32 index, [In] System.Byte busSpeed);
    [DllImport("USBtoI2C.dll")]//, CharSet = CharSet.Auto)]
    private static extern ushort WriteI2C(IntPtr hdl, System.Byte DeviceAddr, [In, Out] System.Byte[] pDataBuf, System.UInt32 DataLen);
    [DllImport("USBtoI2C.dll")]//, CharSet = CharSet.Auto)]
    private static extern ushort ReadI2C(IntPtr hdl, System.Byte DeviceAddr, [In, Out] System.Byte[] pDataBuf, System.UInt32 DataLen);
    [DllImport("USBtoI2C.dll", CharSet = CharSet.Auto)]
    private static extern bool GetUSBDeviceAttachState();
    [DllImport("USBtoI2C.dll", CharSet = CharSet.Auto)]
    private static extern bool CloseDevice(IntPtr hdl);
    [DllImport("USBtoI2C.dll", CharSet = CharSet.Auto)]
    private static extern void CloseAPI();

    /*
    /Public function prototypes
    USBTOI2C_API HANDLE OpenDevice(int index, uint8_t busSpeed);
    USBTOI2C_API HANDLE OpenDeviceWithID(WORD VendorID, WORD ProductID, int index, uint8_t busSpeed);
    USBTOI2C_API BOOL GetUSBDeviceAttachState(void);
    USBTOI2C_API BOOL CloseDevice(HANDLE hdl);
    USBTOI2C_API DWORD WriteI2C(HANDLE hdl, uint8_t DeviceAddr, uint8_t* pDataBuf, DWORD DataLen);
    USBTOI2C_API DWORD ReadI2C(HANDLE hdl, uint8_t DeviceAddr, uint8_t* pDataBuf, DWORD DataLen);
    USBTOI2C_API uint8_t GetLastErrorCode(HANDLE hdl);
    USBTOI2C_API void CloseAPI(void);
     */
    #endregion

    private const ushort VENDOR_ID = 0x0424;
    private const ushort PRODUCT_ID = 0x274c;

    IntPtr USBDeviceHandle = new IntPtr();

    //public enum ISPStep : byte { Standby = 0, Erase = 2, Blanking = 26, Program = 50, Verify = 74, Finished = 100 };
    public enum ISPStep : byte { Standby = 0, Erase = 2, Blanking = 10, Program = 20, Verify = 90, Finished = 100 };
    
    private byte[] BinFile;
    private ISPStep CurrentISPStep = ISPStep.Standby;
    private int ISPProgress = 0;
    private bool isLoadDLL = false;
    private UInt16 BinFileChecksum = 0;
    private bool isLoadBinFile = false;

    public MStarISP()
    {
        try
        {
            USBDeviceHandle = OpenDeviceWithID(VENDOR_ID, PRODUCT_ID, 0, 1);
            isLoadDLL = true;
        }
        catch (DllNotFoundException e)
        {
            System.Windows.Forms.MessageBox.Show(e.Message.ToString());
            isLoadDLL = false;
        }
    }
    ~MStarISP()
    {
        try
        {
            CloseAPI();
        }
        catch (Exception)
        {
        }
    }
    public bool GetIsLoadDLL()
    {
        return isLoadDLL;
    }
    public bool GetIsLoadBinFile()
    {
        return isLoadBinFile;
    }
    private void SetISPStep(ISPStep value)
    {
        CurrentISPStep = value;
    }
    public ISPStep GetISPStep()
    {
        return CurrentISPStep;
    }    
    private void SetISPProgress(int Progress, int TotalProgress)
    {
        int min = 0, max = 0;
        if (Progress <= TotalProgress && Progress >= 0)
        {
            switch (GetISPStep())
            {
                case ISPStep.Standby:
                    max = (int)ISPStep.Erase;
                    min = (int)ISPStep.Standby;
                    break;
                case ISPStep.Erase:
                    max = (int)ISPStep.Blanking;
                    min = (int)ISPStep.Erase;
                    break;
                case ISPStep.Blanking:
                    max = (int)ISPStep.Program;
                    min = (int)ISPStep.Blanking;
                    break;
                case ISPStep.Program:
                    max = (int)ISPStep.Verify;
                    min = (int)ISPStep.Program;
                    break;
                case ISPStep.Verify:
                    max = (int)ISPStep.Finished - 2;
                    min = (int)ISPStep.Verify;
                    break;
                case ISPStep.Finished:
                    max = (int)ISPStep.Finished;
                    min = (int)ISPStep.Finished - 2;
                    break;
                default:
                    break;
            }
            ISPProgress = (int)((Progress + 1) * (max - min) / TotalProgress + min);
        }
    }
    public int GetISPProgress()
    {
        return ISPProgress;
    }
    public bool SetBinFile(string filename)
    {
        BinFile = System.IO.File.ReadAllBytes(@filename);
        if (BinFile.Length > 0)
        {
            CalculateChecksum();
            isLoadBinFile = true;
            return true;
        }
        else
        {
            BinFileChecksum = 0;
            isLoadBinFile = false;
            return false;
        }
    }
    private void CalculateChecksum()
    {
        UInt16 Checksum = 0;
        foreach(byte BF in BinFile)
        {
            Checksum += BF;
        }
        BinFileChecksum = Checksum;
    }
    public UInt16 GetBinFileChecksum()
    {
        return BinFileChecksum;
    }

    private void MStar_SendDDCLeavePMSMode()
    {
        byte[] ISP_CMD = new byte[] { 0x51, 0x84, 0x03, 0xD6, 0x00, 0x01, 0xB8 };
        System.Byte DeviceAddr = 0x6E >> 1;
        System.UInt32 DataLen = 7;
        ushort result = 0;
        if (GetUSBDeviceAttachState() == true)
            result = WriteI2C(USBDeviceHandle, DeviceAddr, ISP_CMD, DataLen);
        Thread.Sleep(5);
    }
    private void MStar_EnterSerialDebugMode()
    {
        byte[] ISP_CMD = new byte[] { 0x53, 0x45, 0x52, 0x44, 0x42 };
        System.Byte DeviceAddr = 0xB2 >> 1;
        System.UInt32 DataLen = 5;
        ushort result = 0;
        if (GetUSBDeviceAttachState() == true)
            result = WriteI2C(USBDeviceHandle, DeviceAddr, ISP_CMD, DataLen);
        Thread.Sleep(5);
    }
    private void MStar_ExitSerialDebugMode()
    {
        byte[] ISP_CMD = new byte[] { 0x45 };
        System.Byte DeviceAddr = 0xB2 >> 1;
        System.UInt32 DataLen = 1;
        ushort result = 0;
        if (GetUSBDeviceAttachState() == true)
            result = WriteI2C(USBDeviceHandle, DeviceAddr, ISP_CMD, DataLen);
        Thread.Sleep(5);
    }
    private void MStar_SetIICBusCtrl()
    {
        {
            byte[] ISP_CMD = new byte[] { 0x35 };
            System.Byte DeviceAddr = 0xB2 >> 1;
            System.UInt32 DataLen = 1;
            ushort result = 0;
            if (GetUSBDeviceAttachState() == true)
                result = WriteI2C(USBDeviceHandle, DeviceAddr, ISP_CMD, DataLen);
            Thread.Sleep(5);
        }
        {
            byte[] ISP_CMD = new byte[] { 0x71 };
            System.Byte DeviceAddr = 0xB2 >> 1;
            System.UInt32 DataLen = 1;
            ushort result = 0;
            if (GetUSBDeviceAttachState() == true)
                result = WriteI2C(USBDeviceHandle, DeviceAddr, ISP_CMD, DataLen);
            Thread.Sleep(5);
        }
    }
    private void MStar_EnterSingleStepMode()
    {
        byte[] ISP_CMD = new byte[] { 0x10, 0xC0, 0xC1, 0x53 };
        System.Byte DeviceAddr = 0xB2 >> 1;
        System.UInt32 DataLen = 4;
        ushort result = 0;
        if (GetUSBDeviceAttachState() == true)
            result = WriteI2C(USBDeviceHandle, DeviceAddr, ISP_CMD, DataLen);
        Thread.Sleep(5);
    }
    private void MStar_ExitSingleStepMode()
    {
        byte[] ISP_CMD = new byte[] { 0x10, 0xC0, 0xC1, 0xFF };
        System.Byte DeviceAddr = 0xB2 >> 1;
        System.UInt32 DataLen = 4;
        ushort result = 0;
        if (GetUSBDeviceAttachState() == true)
            result = WriteI2C(USBDeviceHandle, DeviceAddr, ISP_CMD, DataLen);
        Thread.Sleep(5);
    }
    private void MStar_EnterISPMode()
    {
        byte[] ISP_CMD = new byte[] { 0x4D, 0x53, 0x54, 0x41, 0x52 };
        System.Byte DeviceAddr = 0x92 >> 1;
        System.UInt32 DataLen = 5;
        ushort result = 0;
        if (GetUSBDeviceAttachState() == true)
            result = WriteI2C(USBDeviceHandle, DeviceAddr, ISP_CMD, DataLen);
        Thread.Sleep(5);
    }
    private void MStar_ExitISPMode()
    {
        byte[] ISP_CMD = new byte[] { 0x24 };
        System.Byte DeviceAddr = 0x92 >> 1;
        System.UInt32 DataLen = 1;
        ushort result = 0;
        if (GetUSBDeviceAttachState() == true)
            result = WriteI2C(USBDeviceHandle, DeviceAddr, ISP_CMD, DataLen);
        Thread.Sleep(5);
    }
    private void MStar_DisableHWWriteProtect()
    {
        //{
        //    byte[] ISP_CMD = new byte[] { 0x10, 0x02, 0x26, 0x01 };
        //    System.Byte DeviceAddr = 0xB2 >> 1;
        //    System.UInt32 DataLen = 4;
        //    ushort result = 0;
        //    if (GetUSBDeviceAttachState() == true)
        //        result = WriteI2C(USBDeviceHandle, DeviceAddr, ISP_CMD, DataLen);
        //    Thread.Sleep(5);
        //}
        {
            byte[] ISP_CMD = new byte[] { 0x10, 0x04, 0x26, 0x01 };
            System.Byte DeviceAddr = 0xB2 >> 1;
            System.UInt32 DataLen = 4;
            ushort result = 0;
            if (GetUSBDeviceAttachState() == true)
                result = WriteI2C(USBDeviceHandle, DeviceAddr, ISP_CMD, DataLen);
            Thread.Sleep(5);
        }
        {
            byte[] ISP_CMD = new byte[] { 0x10, 0x02, 0x28, 0x00 };
            System.Byte DeviceAddr = 0xB2 >> 1;
            System.UInt32 DataLen = 4;
            ushort result = 0;
            if (GetUSBDeviceAttachState() == true)
                result = WriteI2C(USBDeviceHandle, DeviceAddr, ISP_CMD, DataLen);
            Thread.Sleep(5);
        }
    }
    private void MStar_EndCMD()
    {
        byte[] ISP_CMD = new byte[] { 0x12 };
        System.Byte DeviceAddr = 0x92 >> 1;
        System.UInt32 DataLen = 1;
        ushort result = 0;
        if (GetUSBDeviceAttachState() == true)
            result = WriteI2C(USBDeviceHandle, DeviceAddr, ISP_CMD, DataLen);
        Thread.Sleep(1);
    }

    private void MStar_FlashDetectType()
    {
        byte temp = 0;
        {
            byte[] ISP_CMD = new byte[] { 0x10, 0x9F };
            System.Byte DeviceAddr = 0x92 >> 1;
            System.UInt32 DataLen = 2;
            ushort result = 0;
            if (GetUSBDeviceAttachState() == true)
                result = WriteI2C(USBDeviceHandle, DeviceAddr, ISP_CMD, DataLen);
            Thread.Sleep(5);
        }
        {
            byte[] ISP_CMD = new byte[] { 0x11 };
            System.Byte DeviceAddr = 0x92 >> 1;
            System.UInt32 DataLen = 1;
            ushort result = 0;
            if (GetUSBDeviceAttachState() == true)
                result = WriteI2C(USBDeviceHandle, DeviceAddr, ISP_CMD, DataLen);
            //Thread.Sleep(5);
        }
        {
            byte[] I2CReadDataBuffer = new byte[10000];
            System.Byte DeviceAddr = 0x92 >> 1;
            System.UInt32 DataLen = 3;
            ushort result = 3;
            if (GetUSBDeviceAttachState() == true)
                result = ReadI2C(USBDeviceHandle, DeviceAddr, I2CReadDataBuffer, DataLen);

            temp = I2CReadDataBuffer[0];
        }

        Thread.Sleep(1);
        MStar_EndCMD();
    }
    private bool MStar_FlashReadStatus()
    {
        bool resultdata = false;
        {
            byte[] ISP_CMD = new byte[] { 0x10, 0x05 };
            System.Byte DeviceAddr = 0x92 >> 1;
            System.UInt32 DataLen = 2;
            ushort result = 0;
            if (GetUSBDeviceAttachState() == true)
                result = WriteI2C(USBDeviceHandle, DeviceAddr, ISP_CMD, DataLen);
            //Thread.Sleep(5);
        }
        {
            byte[] ISP_CMD = new byte[] { 0x11 };
            System.Byte DeviceAddr = 0x92 >> 1;
            System.UInt32 DataLen = 1;
            ushort result = 0;
            if (GetUSBDeviceAttachState() == true)
                result = WriteI2C(USBDeviceHandle, DeviceAddr, ISP_CMD, DataLen);
            //Thread.Sleep(5);
        }
        {
            byte[] I2CReadDataBuffer = new byte[10000];
            System.Byte DeviceAddr = 0x92 >> 1;
            System.UInt32 DataLen = 1;
            ushort result = 3;
            if (GetUSBDeviceAttachState() == true)
            {
                result = ReadI2C(USBDeviceHandle, DeviceAddr, I2CReadDataBuffer, DataLen);
                resultdata = (I2CReadDataBuffer[0] & 0x01) == 0 ? true : false;
            }
        }
        Thread.Sleep(1);
        MStar_EndCMD();
        return resultdata;
    }
    private void MStar_FlashWriteEnable()
    {
        {
            byte[] ISP_CMD = new byte[] { 0x10, 0x06 };
            System.Byte DeviceAddr = 0x92 >> 1;
            System.UInt32 DataLen = 2;
            ushort result = 0;
            if (GetUSBDeviceAttachState() == true)
                result = WriteI2C(USBDeviceHandle, DeviceAddr, ISP_CMD, DataLen);
            Thread.Sleep(1);
            MStar_EndCMD();
        }
    }
    private void MStar_FlashWriteStatus()
    {
        {
            byte[] ISP_CMD = new byte[] { 0x10, 0x01, 0x00 };
            System.Byte DeviceAddr = 0x92 >> 1;
            System.UInt32 DataLen = 3;
            ushort result = 0;
            if (GetUSBDeviceAttachState() == true)
                result = WriteI2C(USBDeviceHandle, DeviceAddr, ISP_CMD, DataLen);
            Thread.Sleep(1);
            MStar_EndCMD();
        }
    }
    private void MStar_FlashWriteDisable()
    {
        {
            byte[] ISP_CMD = new byte[] { 0x10, 0x04 };
            System.Byte DeviceAddr = 0x92 >> 1;
            System.UInt32 DataLen = 2;
            ushort result = 0;
            if (GetUSBDeviceAttachState() == true)
                result = WriteI2C(USBDeviceHandle, DeviceAddr, ISP_CMD, DataLen);
            Thread.Sleep(1);
            MStar_EndCMD();
        }
    }
    private void MStar_FlashSpecialUnprotect()
    {
        {
            byte[] ISP_CMD = new byte[] { 0x10, 0xC3 };
            System.Byte DeviceAddr = 0x92 >> 1;
            System.UInt32 DataLen = 2;
            ushort result = 0;
            if (GetUSBDeviceAttachState() == true)
                result = WriteI2C(USBDeviceHandle, DeviceAddr, ISP_CMD, DataLen);
            Thread.Sleep(1);
        }
        MStar_EndCMD();
        {
            byte[] ISP_CMD = new byte[] { 0x10, 0xA5 };
            System.Byte DeviceAddr = 0x92 >> 1;
            System.UInt32 DataLen = 2;
            ushort result = 0;
            if (GetUSBDeviceAttachState() == true)
                result = WriteI2C(USBDeviceHandle, DeviceAddr, ISP_CMD, DataLen);
            Thread.Sleep(1);
        }
        MStar_EndCMD();
        {
            byte[] ISP_CMD = new byte[] { 0x10, 0xC3 };
            System.Byte DeviceAddr = 0x92 >> 1;
            System.UInt32 DataLen = 2;
            ushort result = 0;
            if (GetUSBDeviceAttachState() == true)
                result = WriteI2C(USBDeviceHandle, DeviceAddr, ISP_CMD, DataLen);
            Thread.Sleep(1);
        }
        MStar_EndCMD();
        {
            byte[] ISP_CMD = new byte[] { 0x10, 0xA5 };
            System.Byte DeviceAddr = 0x92 >> 1;
            System.UInt32 DataLen = 2;
            ushort result = 0;
            if (GetUSBDeviceAttachState() == true)
                result = WriteI2C(USBDeviceHandle, DeviceAddr, ISP_CMD, DataLen);
            Thread.Sleep(1);
        }
        MStar_EndCMD();
    }
    private void MStar_FlashReleaseDeepPowerDown()
    {
        byte[] ISP_CMD = new byte[] { 0x10, 0xAB, 0x00, 0x00, 0x00 };
        System.Byte DeviceAddr = 0x92 >> 1;
        System.UInt32 DataLen = 5;
        ushort result = 0;
        if (GetUSBDeviceAttachState() == true)
            result = WriteI2C(USBDeviceHandle, DeviceAddr, ISP_CMD, DataLen);
        Thread.Sleep(1);
        MStar_EndCMD();
    }

    public bool MStar_StartISP()
    {
        int i = 0, step = 5;
        SetISPStep(ISPStep.Standby);

        if (MStar_USBConnect() == false)
            return false;
        MStar_SendDDCLeavePMSMode();
        SetISPProgress(i++, step);
        MStar_EnterSerialDebugMode();
        SetISPProgress(i++, step);
        MStar_EnterSingleStepMode();
        SetISPProgress(i++, step);
        MStar_SetIICBusCtrl();
        SetISPProgress(i++, step);
        MStar_EnterISPMode();
        SetISPProgress(i++, step);

        MStar_DisableHWWriteProtect();
        SetISPProgress(i++, step);

        //MStar_FlashDetectType();
        //MStar_EndCMD();

        //MStar_FlashReadStatus();
        //MStar_EndCMD();

        return true;
    }
    public bool MStar_EndISP()
    {
        int i = 0, step = 4;
        SetISPStep(ISPStep.Finished);

        if (MStar_USBConnect() == false)
            return false;

        MStar_ExitSingleStepMode();
        SetISPProgress(i++, step);
        MStar_ExitSerialDebugMode();
        SetISPProgress(i++, step);
        MStar_ExitISPMode();
        SetISPProgress(i++, step);
        MStar_EndCMD();
        SetISPProgress(i++, step);

        return true;
    }

    public bool MStar_FlashChipErase()
    {
        int i = 0, step = 2;
        
        SetISPStep(ISPStep.Erase);

        if (MStar_USBConnect() == false)
            return false;

        MStar_FlashWriteEnable();
        MStar_FlashWriteStatus();
        MStar_FlashSpecialUnprotect();
        MStar_FlashWriteEnable();
        MStar_FlashWriteStatus();
        MStar_FlashWriteEnable();
        SetISPProgress(i++, step);

        byte[] ISP_CMD = new byte[] { 0x10, 0xC7 };
        System.Byte DeviceAddr = 0x92 >> 1;
        System.UInt32 DataLen = 2;
        ushort result = 0;
        if (GetUSBDeviceAttachState() == true)
            result = WriteI2C(USBDeviceHandle, DeviceAddr, ISP_CMD, DataLen);
        Thread.Sleep(1);
        MStar_EndCMD();
        SetISPProgress(i++, step);

        return true;
    }
    public bool MStar_FlashProgram()
    {
        SetISPStep(ISPStep.Program);

        if (MStar_USBConnect() == false)
            return false;

        byte[] ISP_CMD = new byte[261];
        Array.Clear(ISP_CMD, 0, ISP_CMD.Length);

        System.Byte DeviceAddr = 0x92 >> 1;
        System.UInt32 DataLen = 256 + 5;
        ushort result = 0;
        int blocksize = 256;

        ISP_CMD[0] = 0x10;
        ISP_CMD[1] = 0x02;
        ISP_CMD[2] = 0x00;  //AddrH
        ISP_CMD[3] = 0x00;  //AddrM
        ISP_CMD[4] = 0x00;  //AddrL
        for (int i = 0; i < BinFile.Length; i = i + blocksize)
        {
            while (true)
            {
                if (MStar_FlashReadStatus() == true)
                    break;
                Thread.Sleep(1);
            }

            MStar_FlashWriteEnable();
            MStar_FlashWriteStatus();
            MStar_FlashSpecialUnprotect();
            MStar_FlashWriteEnable();
            MStar_FlashWriteStatus();
            MStar_FlashWriteDisable();
            MStar_FlashWriteEnable();

            ISP_CMD[2] = (byte)((i >> 16) & 0xFF);  //AddrH
            ISP_CMD[3] = (byte)((i >> 8) & 0xFF);   //AddrM
            ISP_CMD[4] = (byte)(i & 0xFF);          //AddrL

            if (BinFile.Length - i < blocksize)  // last data smaller than block size
            {
                blocksize = (int)BinFile.Length - i;
            }
            Array.Copy(BinFile, i, ISP_CMD, 5, blocksize);

            if (MStar_USBConnect() == true)
                result = WriteI2C(USBDeviceHandle, DeviceAddr, ISP_CMD, DataLen);
            else
                return false;

            MStar_EndCMD();
            SetISPProgress(i, (int)BinFile.Length);
        }

        return true;
    }
    public bool MStar_FlashRead(byte[] RecieveData, int ReciveDataLen)
    {
        uint count = 1;
        for (uint i = 0; i < ReciveDataLen; i = i + count)
        {
            {
                byte[] ISP_CMD = new byte[5];
                ISP_CMD[0] = 0x10;
                ISP_CMD[1] = 0x03;
                ISP_CMD[2] = (byte)((i >> 16) & 0xFF);  //AddrH
                ISP_CMD[3] = (byte)((i >> 8) & 0xFF);   //AddrM
                ISP_CMD[4] = (byte)(i & 0xFF);          //AddrL
                System.Byte DeviceAddr = 0x92 >> 1;
                System.UInt32 DataLen = 5;
                ushort result = 0;

                if (MStar_USBConnect() == true)
                    result = WriteI2C(USBDeviceHandle, DeviceAddr, ISP_CMD, DataLen);
                else
                    return false;
            }
            {
                byte[] ISP_CMD = new byte[] { 0x11 };
                System.Byte DeviceAddr = 0x92 >> 1;
                System.UInt32 DataLen = 1;
                ushort result = 0;
                if (MStar_USBConnect() == true)
                    result = WriteI2C(USBDeviceHandle, DeviceAddr, ISP_CMD, DataLen);
                else
                    return false;
            }
            {
                byte[] RecieveDataTemp = new byte[count];
                System.Byte DeviceAddr = 0x92 >> 1;
                ushort result = 3;
                if (MStar_USBConnect() == true)
                {
                    result = ReadI2C(USBDeviceHandle, DeviceAddr, RecieveDataTemp, count);
                    RecieveDataTemp.CopyTo(RecieveData, i);
                }
                else
                    return false;
                MStar_EndCMD();
            }
            SetISPProgress((int)i, ReciveDataLen);
        }

        return true;
    }

    public bool MStar_FlashBlanking()
    {
        SetISPStep(ISPStep.Blanking);

        //byte[] I2CReadDataBuffer = new byte[524288];
        //MStar_FlashRead(I2CReadDataBuffer, 524288);

        byte[] I2CReadDataBuffer = new byte[4096];
        MStar_FlashRead(I2CReadDataBuffer, 4096);

        bool isBlank = true;
        foreach (byte element in I2CReadDataBuffer)
        {
            if (element != 0xFF)
            {
                isBlank = false;
                break;
            }
        }
        System.IO.File.WriteAllBytes("Blanking.bin", I2CReadDataBuffer);

        return isBlank;
    }
    public bool MStar_FlashVerify()
    {
        SetISPStep(ISPStep.Verify);

        byte[] I2CReadDataBuffer = new byte[524288];
        MStar_FlashRead(I2CReadDataBuffer, 4096);

        bool isVerify = Array.Equals(BinFile, I2CReadDataBuffer);

        System.IO.File.WriteAllBytes("Verifying.bin", I2CReadDataBuffer);

        return isVerify;
    }

    public bool MStar_StartISP2()
    {
        SetISPStep(ISPStep.Standby);
        int step = 3;
        for (int i = 0; i < step; i++)
        {
            if (MStar_USBConnect()==false)
            {
                return false;
            }
            SetISPProgress(i ,step);
            Thread.Sleep(100);
        }
        return true;
    }
    public bool MStar_EndISP2()
    {
        SetISPStep(ISPStep.Finished);

        int step = 3;
        for (int i = 0; i < step; i++)
        {
            if (MStar_USBConnect() == false)
            {
                return false;
            }
            SetISPProgress(i, step);
            Thread.Sleep(100);
        }
        return true;
    }

    public bool MStar_FlashChipErase2()
    {
        SetISPStep(ISPStep.Erase);

        int step = 60;
        for (int i = 0; i < step; i++)
        {
            if (MStar_USBConnect() == false)
            {
                return false;
            }
            SetISPProgress(i, step);
            Thread.Sleep(100);
        }
        return true;
    }
    public bool MStar_FlashProgram2()
    {
        SetISPStep(ISPStep.Program);

        int step = 60;
        for (int i = 0; i < step; i++)
        {
            if (MStar_USBConnect() == false)
            {
                return false;
            }
            SetISPProgress(i, step);
            Thread.Sleep(100);
        }
        return true;
    }
    
    public bool MStar_FlashBlanking2()
    {
        SetISPStep(ISPStep.Blanking);

        int step = 50;
        for (int i = 0; i < step; i++)
        {
            if (MStar_USBConnect() == false)
            {
                return false;
            }
            SetISPProgress(i, step);
            Thread.Sleep(100);
        }
        return true;
    }
    public bool MStar_FlashVerify2()
    {
        SetISPStep(ISPStep.Verify);

        int step = 50;
        for (int i = 0; i < step; i++)
        {
            if (MStar_USBConnect() == false)
            {
                return false;
            }
            SetISPProgress(i, step);
            Thread.Sleep(100);
        }
        return true;
    }

    public bool MStar_ISPProcedure()
    {
        MStar_StartISP();

        MStar_FlashChipErase();
        //MStar_FlashBlanking();
        MStar_FlashProgram();
        //MStar_FlashVerify();

        MStar_EndISP();

        return true;
    }

    public bool MStar_USBConnect()
    {
        //try
        //{
        //    return GetUSBDeviceAttachState();
        //}
        //catch (Exception)
        //{
        //    return false;
        //}
        return true;
    }
}