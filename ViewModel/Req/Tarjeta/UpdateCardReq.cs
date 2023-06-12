namespace workcube_pagos.ViewModel.Req.Tarjeta
{
    public class UpdateCardReq
    {
        public int IdCliente { get; set; }
        public string CardStripeId { get; set; }
        public string Name { get; set; }
        public int ExpMonth { get; set; }
        public int ExpYear { get; set; }
    }
}
