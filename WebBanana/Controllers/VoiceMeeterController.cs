using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VoiceMeeterClasses;

namespace WebBanana.Controllers
{
    [Produces("application/json")]
    [Route("VoiceMeeter")]
    public class VoiceMeeterController : Controller
    {
        [Route("isdirty")]
        public JsonResult GetDirty(string parameter)
        {
            return Json((Boolean)VoiceMeeterConnector.Instance.IsParameterDirty());
        }

        [Route("script/{script}")]
        public string SetScript(string script)
        {
            return Convert.ToString(VoiceMeeterConnector.Instance.SetParameters(script));
        }

        [Route("strip/{parameter}", Name = "GetStrip")]
        public string GetStrip(string parameter)
        {
            Strip result = VoiceMeeterConnector.Instance.GetStrip(0);
            string output = JsonConvert.SerializeObject(result);

            return output;
        }

        [Route("", Name = "GetVoiceMeeter")]
        public JsonResult GetVoiceMeeter(string parameter)
        {
            VoiceMeeter result = VoiceMeeterConnector.Instance.GetVoiceMeeter();
            return Json(result);
        }

        [Route("level/{type}/{channel}")]
        public string GetLevel(int type, int channel)
        {
            return Convert.ToString(VoiceMeeterConnector.Instance.GetLevel(type, channel));
        }

        [Route("parameter/{parameter}/{newValue}")]
        public string SetParameter(string parameter, float newValue)
        {
            return Convert.ToString(VoiceMeeterConnector.Instance.SetParameter(parameter, newValue));
        }
       
    }
}
