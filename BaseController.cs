using Karasoft.Mvc.Html;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using Microsoft.Owin.Security;
using System.Web;

namespace Karasoft.Mvc
{
    public class BaseController : Controller
    {
        public void Success(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Success, message, dismissable);
        }

        public void Success(IEnumerable<string> messages, bool dismissable = false)
        {
            foreach (var item in messages)
            {
                Success(string.Format("<li>{0}</li>", item), dismissable);
            }
        }

        public void Success(ModelStateDictionary ModelState, bool dismissable = false)
        {
            var allErrors = ModelState.Values.SelectMany(v => v.Errors);
            Success(allErrors.Select(ee => ee.ErrorMessage));
        }

        public void Information(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Information, message, dismissable);
        }

        public void Information(IEnumerable<string> messages, bool dismissable = false)
        {
            foreach (var item in messages)
            {
                Information(string.Format("<li>{0}</li>", item), dismissable);
            }
        }

        public void Information(ModelStateDictionary ModelState, bool dismissable = false)
        {
            var allErrors = ModelState.Values.SelectMany(v => v.Errors);
            Information(allErrors.Select(ee => ee.ErrorMessage));
        }

        public void Warning(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Warning, message, dismissable);
        }

        public void Warning(IEnumerable<string> messages, bool dismissable = false)
        {
            foreach (var item in messages)
            {
                Warning(string.Format("<li>{0}</li>", item), dismissable);
            }
        }

        public void Warning(ModelStateDictionary ModelState, bool dismissable = false)
        {
            var allErrors = ModelState.Values.SelectMany(v => v.Errors);
            Warning(allErrors.Select(ee => ee.ErrorMessage));
        }

        public void Danger(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Danger, message, dismissable);
        }

        public void Danger(IEnumerable<string> messages, bool dismissable = false)
        {
            foreach (var item in messages)
            {
                Danger(string.Format("<li>{0}</li>", item), dismissable);
            }
        }

        public void Danger(ModelStateDictionary ModelState, bool dismissable = false)
        {
            var allErrors = ModelState.Values.SelectMany(v => v.Errors);
            Danger(allErrors.Select(ee => ee.ErrorMessage));
        }



        private void AddAlert(string alertStyle, string message, bool dismissable)
        {
            var alerts = TempData.ContainsKey(Alert.TempDataKey)
                ? (List<Alert>)TempData[Alert.TempDataKey]
                : new List<Alert>();

            alerts.Add(new Alert
            {
                AlertStyle = alertStyle,
                Message = message,
                Dismissable = dismissable
            });

            TempData[Alert.TempDataKey] = alerts;
        }

    }

    public class ChallengeResult : HttpUnauthorizedResult
    {
        private const string XsrfKey = "XsrfId";
        public ChallengeResult(string provider, string redirectUri)
            : this(provider, redirectUri, null)
        {
        }

        public ChallengeResult(string provider, string redirectUri, string userId)
        {
            LoginProvider = provider;
            RedirectUri = redirectUri;
            UserId = userId;
        }

        public string LoginProvider { get; set; }
        public string RedirectUri { get; set; }
        public string UserId { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
            if (UserId != null)
            {
                properties.Dictionary[XsrfKey] = UserId;
            }
            context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
        }
    }
}
