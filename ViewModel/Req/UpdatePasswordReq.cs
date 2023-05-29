namespace workcube_pagos.ViewModel.Req
{
    public class UpdatePasswordReq
    {
        public string Id { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set;}
    }
}
