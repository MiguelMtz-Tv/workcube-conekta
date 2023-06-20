namespace workcube_pagos.ViewModel.Req.Pago
{
    public class ConfirmationEmailReq
    {
        public DateTime Fecha       { get; set; }
        public int IdCliente        { get; set; }
        public string IdAspNetUser  { get; set; }
        public string ServicioName  { get; set; } 
        public long Monto           { get; set; }
        public long Descuento       { get; set; }
        public long Total           { get; set; }
        public string Last4         { get; set; }
        public string CardHolder    { get; set; }
    }
}
