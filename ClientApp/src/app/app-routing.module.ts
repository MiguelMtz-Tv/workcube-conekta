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

const pahts: Routes = [
    { path: '', component: LoginComponent, pathMatch: 'full' },
    { path: 'registro', component: SingupComponent },
    { path: 'servicios', component: ServiciosComponent },
    { path: 'tarjetas', component: TarjetasComponent },
    { path: 'perfil', component: PerfilComponent },
    { path: 'historial', component: HistorialPagosComponent },
    { path: 'pagar', component: PagarComponent },
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