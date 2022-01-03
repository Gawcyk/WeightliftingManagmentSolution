
using Prism.Services.Dialogs;

using WeightliftingManagment.Core.Constans;
using WeightliftingManagment.Domain.Model;

namespace WeightliftingManagment.Core.Dialogs
{
    public static class DialogServiceExtensions
    {
        public static void ShowNotification(this IDialogService dialogService, string title, string message, int closeTime = 10) => dialogService.ShowDialog(DialogName.Notification, new DialogParameters($"Title={title}&Message={message}&CloseTime={closeTime}&DialogType={DialogType.Information}"), null);

        public static void ShowAlert(this IDialogService dialogService, string title, string message, int closeTime = 10) => dialogService.ShowDialog(DialogName.Notification, new DialogParameters($"Title={title}&Message={message}&CloseTime={closeTime}&DialogType={DialogType.Alert}"), null);

        public static bool ShowQuestion(this IDialogService dialogService, string title, string message, int closeTime = 10)
        {
            var resultBool = false;
            dialogService.ShowDialog(DialogName.Question, new DialogParameters($"Title={title}&Message={message}&CloseTime={closeTime}"), (result) => {
                if (result.Result == ButtonResult.Yes)
                {
                    resultBool = true;
                }
            });
            return resultBool;
        }

        public static Participant ShowAddParticipant(this IDialogService dialogService, string title)
        {
            var participant = new Participant();
            dialogService.ShowDialog(DialogName.AddParticipant, new DialogParameters($"Title={title}"), (result) => {
                if (result.Result == ButtonResult.OK)
                {
                    participant = result.Parameters.GetValue<Participant>("NewParticipant");
                }
            });
            return participant;
        }

        public static string ShowColorDialog(this IDialogService dialogService)
        {
            var hex = "";
            dialogService.ShowDialog(DialogName.Color, null, result => {
                if (result.Result == ButtonResult.OK)
                {
                    hex = result.Parameters.GetValue<string>("HEX");
                }
            });
            return hex;
        }
    }
}
