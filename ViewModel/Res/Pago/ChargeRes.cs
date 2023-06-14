namespace workcube_pagos.ViewModel.Res.Pago
{
    public class ChargeRes
    {
        public DateTime Fecha           { get; set; }
        public int IdServicio           { get; set; }
        public int IdCliente            { get; set; }
        public string IdStripeCharge    { get; set; }  
        public string IdStripeCard      { get; set; }
        public long Monto               { get; set; }
        public long Descuento           { get; set; }
        public long Total               { get; set; }    

    }
}
