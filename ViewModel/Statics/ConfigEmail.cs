using Workcube.ViewModels;

namespace workcube_pagos.ViewModel.Statics
{
    public class ConfigEmail
    {
        public static ModelEmail Data()
        {
            ModelEmail objEmail = new ModelEmail();

            objEmail.EmailDisplayName = "Oil Field Trainning";
            objEmail.EmailEnabled =     true;
            objEmail.EmailHost =        "workcube.com.mx";
            objEmail.EmailPassword =    "$W0rkC6b3@NR";
            objEmail.EmailPort =        587;
            objEmail.EmailUser =        "no-reply@workcube.com.mx";

            return objEmail;
        }
    }
}
