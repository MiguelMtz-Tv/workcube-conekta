import { NgModule } from '@angular/core'
import { CommonModule } from '@angular/common'
import { RouterModule, Routes } from '@angular/router'
import { LoginComponent } from './auth/login/login.component'
import { SingupComponent } from './auth/singup/singup.component'
import { ServiciosComponent } from './pages/servicios/servicios.component'
import { TarjetasComponent } from './pages/tarjetas/tarjetas.component'
import { PerfilComponent } from './pages/perfil/perfil.component'
import { HistorialPagosComponent } from './pages/historial-pagos/historial-pagos.component'
import { PagarComponent } from './pages/pagar/pagar.component'
import { PagesModule } from './pages/pages.module'
import { AuthGuard } from './auth/guards/auth.guard'
//import { ClientComponent } from './pages/client/client.component'
import { PagoConfirmadoComponent } from './pages/pago-confirmado/pago-confirmado.component'

const pahts: Routes = [
    { path: '',                 component: LoginComponent,              pathMatch: 'full'       },
    { path: 'registro',         component: SingupComponent                                      },
    { path: 'servicios',        component: ServiciosComponent,          canActivate:[AuthGuard] },
    { path: 'tarjetas',         component: TarjetasComponent,           canActivate:[AuthGuard] },
    { path: 'perfil',           component: PerfilComponent,             canActivate:[AuthGuard] },
    { path: 'historial/:id',    component: HistorialPagosComponent,     canActivate:[AuthGuard] },
    { path: 'pagar/:id',        component: PagarComponent,              canActivate:[AuthGuard] },
    { path: 'pago-confirmado',  component: PagoConfirmadoComponent,     canActivate:[AuthGuard] },
    //{ path: 'cliente',          component: ClientComponent },
]


@NgModule({
    imports:[
        CommonModule,
        RouterModule.forRoot(pahts),
        PagesModule,
    ],
    exports:[
        RouterModule
    ]
})

export class AppRoutingModule { }