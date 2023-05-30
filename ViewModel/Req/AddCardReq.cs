namespace workcube_pagos.ViewModel.Req
{
    public class AddCardReq
    {
        public int IdCliente { get; set; }
        public string name { get; set; }
        public string number { get; set; }
        public string exp_month { get; set; }  
        public string exp_year { get; set;}
        public string cvc_check { get; set; }

    }
}
