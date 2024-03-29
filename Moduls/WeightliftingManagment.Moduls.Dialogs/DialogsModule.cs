﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Prism.Ioc;
using Prism.Modularity;

using WeightliftingManagment.Core.Constans;
using WeightliftingManagment.Moduls.Dialogs.Views;
using WeightliftingManagment.Moduls.Dialogs.Window;

namespace WeightliftingManagment.Moduls.Dialogs
{
    public class DialogsModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialogWindow<DialogWindow>();
            containerRegistry.RegisterDialog<NotificationDialog>(DialogName.Notification);
            containerRegistry.RegisterDialog<QuestionDialog>(DialogName.Question);
            //containerRegistry.RegisterDialog<ColorDialog>(DialogName.ColorDialog);
            //containerRegistry.RegisterDialog<PrintDialog>(DialogName.PrintPreviewDialog);
        }
    }
}
