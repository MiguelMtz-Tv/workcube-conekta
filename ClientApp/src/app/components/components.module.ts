import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { CuponComponent } from "./cupon/cupon.component";
import { CuponsFormComponent } from "./cupons-form/cupons-form.component";
import { DropdownComponent } from "./dropdown/dropdown.component";
import { NavbarMenuComponent } from "./navbar-menu/navbar-menu.component";
import { PageTitleComponent } from "./page-title/page-title.component";
import { PaymentCardComponent } from "./payment-card/payment-card.component";
import { PaymentModalComponent } from "./payment-modal/payment-modal.component";
import { ServiceCardComponent } from "./service-card/service-card.component";
import { LibrariesModule } from "../shared/libraries.module";
import { MaterialModule } from "../shared/material.module";
import { AddCardComponent } from './dialogs/add-card/add-card.component';
import { ConfirmarPagoComponent } from './dialogs/confirmar-pago/confirmar-pago.component';
import { CancelServiceComponent } from './dialogs/cancel-service/cancel-service.component';
import { CuponsDialogComponent } from './dialogs/cupons-dialog/cupons-dialog.component';
import { DeleteCardComponent } from './dialogs/delete-card/delete-card.component';


@NgModule({
    declarations:[
        CuponComponent,
        CuponsFormComponent,
        DropdownComponent,
        NavbarMenuComponent,
        PageTitleComponent,
        PaymentCardComponent,
        PaymentModalComponent,
        ServiceCardComponent,
        AddCardComponent,
        ConfirmarPagoComponent,
        CancelServiceComponent,
        CuponsDialogComponent,
        DeleteCardComponent,
    ],
    imports:[
        CommonModule,
        LibrariesModule,
        MaterialModule
    ],
    exports:[
        CuponComponent,
        CuponsFormComponent,
        DropdownComponent,
        NavbarMenuComponent,
        PageTitleComponent,
        PaymentCardComponent,
        PaymentModalComponent,
        ServiceCardComponent,
    ]
})

export class ComponentsModule {}