import { NgModule } from '@angular/core'
import { CommonModule } from '@angular/common'
import { HistorialPagosComponent } from './historial-pagos/historial-pagos.component'
import { PagarComponent } from './pagar/pagar.component'
import { PerfilComponent } from './perfil/perfil.component'
import { ServiciosComponent } from './servicios/servicios.component'
import { TarjetasComponent } from './tarjetas/tarjetas.component'
import { LibrariesModule } from '../shared/libraries.module'
import { MaterialModule } from '../shared/material.module'
import { ComponentsModule } from '../components/components.module'
import { LoginComponent } from '../auth/login/login.component'
import { SingupComponent } from '../auth/singup/singup.component'
import { LayoutsModule } from '../layouts/layout.module'

@NgModule({
    declarations:[
        HistorialPagosComponent,
        PagarComponent,
        PerfilComponent,
        ServiciosComponent,
        TarjetasComponent,
        LoginComponent,
        SingupComponent,
    ],
    imports:[
        CommonModule,
        LibrariesModule,
        MaterialModule,
        ComponentsModule,
        LayoutsModule
    ],
    exports:[
        HistorialPagosComponent,
        PagarComponent,
        PerfilComponent,
        ServiciosComponent,
        TarjetasComponent,
    ]
})

export class PagesModule { }