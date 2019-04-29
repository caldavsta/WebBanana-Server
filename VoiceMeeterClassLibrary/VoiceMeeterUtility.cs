using System;
using System.Collections.Generic;
using System.Text;

namespace VoiceMeeterClasses
{
    public static class VoiceMeeterUtility
    {
        public static string GetScriptFromDifferences(VoiceMeeter vm1, VoiceMeeter vm2)
        {
            string result = string.Empty;
            for (int i = 0; i < vm1.Strips.Length; i++)
            {
                result += GetScriptFromDifferences(vm1.Strips[i], vm2.Strips[i], i);
            }
            return result;
        }
        public static string GetScriptFromDifferences(Strip strip1, Strip strip2, int strip1Id)
        {
            string result = string.Empty;
            if (strip1.Mono != strip2.Mono)
            {
                result += String.Format("Strip[{0}].Mono = {1};", strip1Id, strip1.Mono);
            }
            if (strip1.Mute != strip2.Mute)
            {
                result += String.Format("Strip[{0}].Mute = {1};", strip1Id, strip1.Mute);
            }
            if (strip1.Solo != strip2.Solo)
            {
                result += String.Format("Strip[{0}].Solo = {1};", strip1Id, strip1.Solo);
            }
            if (strip1.Mc != strip2.Mc)
            {
                result += String.Format("Strip[{0}].MC = {1};", strip1Id, strip1.Mc);
            }
            if (strip1.Gain != strip2.Gain)
            {
                result += String.Format("Strip[{0}].Gain = {1};", strip1Id, strip1.Gain);
            }
            if (strip1.PanX != strip2.PanX)
            {
                result += String.Format("Strip[{0}].Pan_x= {1};", strip1Id, strip1.PanX);
            }
            if (strip1.PanY != strip2.PanY)
            {
                result += String.Format("Strip[{0}].Pan_y = {1};", strip1Id, strip1.PanY);
            }
            if (strip1.ColorX != strip2.ColorX)
            {
                result += String.Format("Strip[{0}].ColorX = {1};", strip1Id, strip1.ColorX);
            }
            if (strip1.ColorY != strip2.ColorY)
            {
                result += String.Format("Strip[{0}].ColorY = {1};", strip1Id, strip1.ColorY);
            }
            if (strip1.FxX != strip2.FxX)
            {
                result += String.Format("Strip[{0}].fx_x = {1};", strip1Id, strip1.FxX);
            }
            if (strip1.FxY != strip2.FxY)
            {
                result += String.Format("Strip[{0}].x_y = {1};", strip1Id, strip1.FxY);
            }
            if (strip1.Audibility != strip2.Audibility)
            {
                result += String.Format("Strip[{0}].Audibility = {1};", strip1Id, strip1.Audibility);
            }
            if (strip1.Comp != strip2.Comp)
            {
                result += String.Format("Strip[{0}].Comp = {1};", strip1Id, strip1.Comp);
            }
            if (strip1.Gate != strip2.Gate)
            {
                result += String.Format("Strip[{0}].Gate = {1};", strip1Id, strip1.Gate);
            }
            if (strip1.EqGain1 != strip2.EqGain1)
            {
                result += String.Format("Strip[{0}].EQGain1 = {1};", strip1Id, strip1.EqGain1);
            }
            if (strip1.EqGain2 != strip2.EqGain2)
            {
                result += String.Format("Strip[{0}].EQGain2 = {1};", strip1Id, strip1.EqGain2);
            }
            if (strip1.EqGain3 != strip2.EqGain3)
            {
                result += String.Format("Strip[{0}].EQGain3 = {1};", strip1Id, strip1.EqGain3);
            }
            if (strip1.label != strip2.label)
            {
                result += String.Format("Strip[{0}].Label = {1};", strip1Id, strip1.label);
            }
            if (strip1.A1 != strip2.A1)
            {
                result += String.Format("Strip[{0}].A1 = {1};", strip1Id, strip1.A1);
            }
            if (strip1.A2 != strip2.A2)
            {
                result += String.Format("Strip[{0}].A2 = {1};", strip1Id, strip1.A2);
            }
            if (strip1.A3 != strip2.A3)
            {
                result += String.Format("Strip[{0}].A3 = {1};", strip1Id, strip1.A3);
            }
            if (strip1.B1 != strip2.B1)
            {
                result += String.Format("Strip[{0}].B1 = {1};", strip1Id, strip1.B1);
            }
            if (strip1.B2 != strip2.B2)
            {
                result += String.Format("Strip[{0}].B2 = {1};", strip1Id, strip1.B2);
            }
            return result;
        }
    }
}
