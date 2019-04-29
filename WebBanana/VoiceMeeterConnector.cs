using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using VoiceMeeterClasses;

namespace WebBanana
{
    public sealed class VoiceMeeterConnector
    {
        private static VoiceMeeterConnector instance = null;
        private static readonly object padlock = new object();

        private VoiceMeeterConnector()
        {
        }

        public VoiceMeeter GetVoiceMeeter()
        {
            VoiceMeeter result = new VoiceMeeter();
            int busCount = 0;
            int stripCount = 0;

            //determine how to build the VoiceMeeter based on version;
            switch (GetVoiceMeeterType())
            {
                case 1: 
                    //voicemeeter
                    busCount = 2;
                    stripCount = 3;
                    break;
                case 2: 
                    //voicemeeter banana
                    busCount = 5;
                    stripCount = 5;
                    break;
            }

            //get strips
            result.Strips = new Strip[stripCount];
            for (int i = 0; i < stripCount; i++)
            {
                result.Strips[i] = GetStrip(i);
            }

            //get busses
            result.Busses = new Bus[busCount];
            for (int i = 0; i < stripCount; i++)
            {
                result.Busses[i] = new Bus();
            }



            return result;
        }

        public Strip GetStrip(int id)
        {
            Strip result = new Strip();
            result.Gain = GetStripParameter("Gain", id);
            result.PanX = GetStripParameter("Pan_x", id);
            result.PanY = GetStripParameter("Pan_y", id);
            result.ColorX = GetStripParameter("Color_x", id);
            result.ColorY = GetStripParameter("Color_y", id);
            result.FxX = GetStripParameter("fx_x", id);
            result.FxY = GetStripParameter("fx_y", id);
            result.Audibility = GetStripParameter("Audibility", id);
            result.Comp = GetStripParameter("Comp", id);
            result.Gate = GetStripParameter("Gate", id);
            result.EqGain1 = GetStripParameter("EQGain1", id);
            result.EqGain2 = GetStripParameter("EQGain2", id);
            result.EqGain3 = GetStripParameter("EQGain3", id);

            result.Mono = Convert.ToBoolean(GetStripParameter("Mono", id));
            result.Mute = Convert.ToBoolean(GetStripParameter("Mute", id));
            result.Solo = Convert.ToBoolean(GetStripParameter("Solo", id));
            result.Mc = Convert.ToBoolean(GetStripParameter("MC", id));
            result.A1 = Convert.ToBoolean(GetStripParameter("A1", id));
            result.A2 = Convert.ToBoolean(GetStripParameter("A2", id));
            result.A3 = Convert.ToBoolean(GetStripParameter("A3", id));
            result.B1 = Convert.ToBoolean(GetStripParameter("B1", id));
            result.B2 = Convert.ToBoolean(GetStripParameter("B2", id));

            return result;
        }


        public Bus GetBus(int id)
        {
            return new Bus();
        }

        public static VoiceMeeterConnector Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new VoiceMeeterConnector();
                        instance.Login();
                    }
                    return instance;
                }

            }
        }

        public int Login()
        {
            return NativeFunctions.VBVMR_Login();
        }

        public int Logout()
        {
            return NativeFunctions.VBVMR_Logout();
        }

        public int GetVoiceMeeterVersion()
        {
            int version = 0;
            NativeFunctions.VBVMR_GetVoicemeeterVersion(ref version);

            return version;
        }

        public int GetVoiceMeeterType()
        {
            int type = 0;
            NativeFunctions.VBVMR_GetVoicemeeterType(ref type);
            Console.WriteLine("GetVoiceMeeterType: " + type);
            return type;
        }

        public float GetStripParameter(string parameter, int id)
        {
            return GetParameterFloat(String.Format("Strip[{0}].{1}", id, parameter));
        }

        public float GetBusParameter(string parameter, int id)
        {
            return GetParameterFloat(String.Format("Bus[{0}].{1}", id, parameter));
        }

        public float GetParameterFloat(string parameter)
        {
            float parameterValue = 0.0f;
            int result = NativeFunctions.VBVMR_GetParameterFloat(parameter, ref parameterValue);

            if (result != 0)
            {
                Console.WriteLine("GetParameterFloat(): Voicemeeter returned " + result + " for parameter " + parameter);
            }

            Console.WriteLine("GetParameterFloat: " + parameter + " = " + parameterValue);
            return parameterValue;
        }

        public bool IsParameterDirty()
        {
            if (NativeFunctions.VBVMR_IsParametersDirty() == 1)
            {
                return true;
            } else
            {
                return false;
            }
        }

        public float GetLevel(int type, int channel)
        {
            float level = 0.0f;
            long result = NativeFunctions.VBVMR_GetLevel(type, channel, ref level);
            if (result != 0)
            {
                Console.WriteLine("GetLevel returned error code " + result);
            }
            return level;
        }

        public bool SetParameter(string parameter, float newSetting)
        {
            int result = NativeFunctions.VBVMR_SetParameterFloat(parameter, newSetting);
            return (result == 0);
        }

        public int SetParameters(string script)
        {
            int result = NativeFunctions.VBVMR_SetParameters(script);
            return result;
        }

    }
    public class NativeFunctions
    {
        const string DllPath = "C:\\Program Files (x86)\\VB\\Voicemeeter\\VoicemeeterRemote64.dll";

        /// Return Type: int
        [System.Runtime.InteropServices.DllImportAttribute(DllPath, EntryPoint = "VBVMR_Login", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern int VBVMR_Login();


        /// Return Type: int
        [System.Runtime.InteropServices.DllImportAttribute(DllPath, EntryPoint = "VBVMR_Logout", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern int VBVMR_Logout();


        /// Return Type: int
        ///pType: int*
        [System.Runtime.InteropServices.DllImportAttribute(DllPath, EntryPoint = "VBVMR_GetVoicemeeterType", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern int VBVMR_GetVoicemeeterType(ref int pType);


        /// Return Type: int
        ///vType: int
        [System.Runtime.InteropServices.DllImportAttribute(DllPath, EntryPoint = "VBVMR_RunVoicemeeter", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern int VBVMR_RunVoicemeeter(int vType);


        /// Return Type: int
        ///pVersion: int*
        [System.Runtime.InteropServices.DllImportAttribute(DllPath, EntryPoint = "VBVMR_GetVoicemeeterVersion", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern int VBVMR_GetVoicemeeterVersion(ref int pVersion);


        /// Return Type: int
        [System.Runtime.InteropServices.DllImportAttribute(DllPath, EntryPoint = "VBVMR_IsParametersDirty", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern int VBVMR_IsParametersDirty();


        /// Return Type: int
        ///szParamName: char*
        ///pValue: float*
        [System.Runtime.InteropServices.DllImportAttribute(DllPath, EntryPoint = "VBVMR_GetParameterFloat", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern int VBVMR_GetParameterFloat(string szParamName, ref float pValue);


        /// Return Type: int
        ///szParamName: char*
        ///szString: char*
        [System.Runtime.InteropServices.DllImportAttribute(DllPath, EntryPoint = "VBVMR_GetParameterStringA", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern int VBVMR_GetParameterStringA(System.IntPtr szParamName, System.IntPtr szString);


        /// Return Type: int
        ///szParamName: char*
        ///wszString: unsigned short*
        [System.Runtime.InteropServices.DllImportAttribute(DllPath, EntryPoint = "VBVMR_GetParameterStringW", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern int VBVMR_GetParameterStringW(System.IntPtr szParamName, ref ushort wszString);


        /// Return Type: int
        ///nType: int
        ///nuChannel: int
        ///pValue: float*
        [System.Runtime.InteropServices.DllImportAttribute(DllPath, EntryPoint = "VBVMR_GetLevel", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern int VBVMR_GetLevel(int nType, int nuChannel, ref float pValue);


        /// Return Type: int
        ///pMIDIBuffer: unsigned char*
        ///nbByteMax: int
        [System.Runtime.InteropServices.DllImportAttribute(DllPath, EntryPoint = "VBVMR_GetMidiMessage", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern int VBVMR_GetMidiMessage(System.IntPtr pMIDIBuffer, int nbByteMax);


        /// Return Type: int
        ///szParamName: char*
        ///Value: float
        [System.Runtime.InteropServices.DllImportAttribute(DllPath, EntryPoint = "VBVMR_SetParameterFloat", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern int VBVMR_SetParameterFloat(string szParamName, float Value);


        /// Return Type: int
        ///szParamName: char*
        ///szString: char*
        [System.Runtime.InteropServices.DllImportAttribute(DllPath, EntryPoint = "VBVMR_SetParameterStringA", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern int VBVMR_SetParameterStringA(System.IntPtr szParamName, System.IntPtr szString);


        /// Return Type: int
        ///szParamName: char*
        ///wszString: unsigned short*
        [System.Runtime.InteropServices.DllImportAttribute(DllPath, EntryPoint = "VBVMR_SetParameterStringW", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern int VBVMR_SetParameterStringW(System.IntPtr szParamName, ref ushort wszString);


        /// Return Type: int
        ///szParamScript: char*
        [System.Runtime.InteropServices.DllImportAttribute(DllPath, EntryPoint = "VBVMR_SetParameters", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern int VBVMR_SetParameters(string szParamScript);


        /// Return Type: int
        ///szParamScript: unsigned short*
        [System.Runtime.InteropServices.DllImportAttribute(DllPath, EntryPoint = "VBVMR_SetParametersW", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern int VBVMR_SetParametersW(ref ushort szParamScript);


        /// Return Type: int
        [System.Runtime.InteropServices.DllImportAttribute(DllPath, EntryPoint = "VBVMR_Output_GetDeviceNumber", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern int VBVMR_Output_GetDeviceNumber();


        /// Return Type: int
        ///zindex: int
        ///nType: int*
        ///szDeviceName: char*
        ///szHardwareId: char*
        [System.Runtime.InteropServices.DllImportAttribute(DllPath, EntryPoint = "VBVMR_Output_GetDeviceDescA", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern int VBVMR_Output_GetDeviceDescA(int zindex, ref int nType, System.IntPtr szDeviceName, System.IntPtr szHardwareId);


        /// Return Type: int
        ///zindex: int
        ///nType: int*
        ///wszDeviceName: unsigned short*
        ///wszHardwareId: unsigned short*
        [System.Runtime.InteropServices.DllImportAttribute(DllPath, EntryPoint = "VBVMR_Output_GetDeviceDescW", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern int VBVMR_Output_GetDeviceDescW(int zindex, ref int nType, ref ushort wszDeviceName, ref ushort wszHardwareId);


        /// Return Type: int
        [System.Runtime.InteropServices.DllImportAttribute(DllPath, EntryPoint = "VBVMR_Input_GetDeviceNumber", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern int VBVMR_Input_GetDeviceNumber();


        /// Return Type: int
        ///zindex: int
        ///nType: int*
        ///szDeviceName: char*
        ///szHardwareId: char*
        [System.Runtime.InteropServices.DllImportAttribute(DllPath, EntryPoint = "VBVMR_Input_GetDeviceDescA", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern int VBVMR_Input_GetDeviceDescA(int zindex, ref int nType, System.IntPtr szDeviceName, System.IntPtr szHardwareId);


        /// Return Type: int
        ///zindex: int
        ///nType: int*
        ///wszDeviceName: unsigned short*
        ///wszHardwareId: unsigned short*
        [System.Runtime.InteropServices.DllImportAttribute(DllPath, EntryPoint = "VBVMR_Input_GetDeviceDescW", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern int VBVMR_Input_GetDeviceDescW(int zindex, ref int nType, ref ushort wszDeviceName, ref ushort wszHardwareId);


    }
}
