using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Prism.Services.Dialogs;

using WeightliftingManagment.Core.Constans;

namespace WeightliftingManagment.Core.Dialogs
{
    public static class DialogServiceExtensions
    {
        public static void ShowNotification(this IDialogService dialogService, string title, string message, int closeTime = 10) => dialogService.ShowDialog(DialogName.NotificationDialog, new DialogParameters($"Title={title}&Message={message}&CloseTime={closeTime}&DialogType={DialogType.Information}"), null);

        public static void ShowAlert(this IDialogService dialogService, string title, string message, int closeTime = 10) => dialogService.ShowDialog(DialogName.NotificationDialog, new DialogParameters($"Title={title}&Message={message}&CloseTime={closeTime}&DialogType={DialogType.Alert}"), null);

        public static bool ShowQuestion(this IDialogService dialogService, string title, string message, int closeTime = 10)
        {
            var resultBool = false;
            dialogService.ShowDialog(DialogName.QuestionDialog, new DialogParameters($"Title={title}&Message={message}&CloseTime={closeTime}"), (result) => {
                if (result.Result == ButtonResult.Yes)
                {
                    resultBool = true;
                }
            });
            return resultBool;
        }

        public static string ShowColorDialog(this IDialogService dialogService)
        {
            var hex = "";
            dialogService.ShowDialog(DialogName.ColorDialog, null, result => {
                if (result.Result == ButtonResult.OK)
                {
                    hex = result.Parameters.GetValue<string>("HEX");
                }
            });
            return hex;
        }
    }
}
