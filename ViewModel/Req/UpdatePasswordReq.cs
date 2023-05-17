namespace workcube_pagos.ViewModel.Req
{
    public class UpdatePasswordReq
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set;}
    }
}
