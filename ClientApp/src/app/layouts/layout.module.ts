import { NgModule } from '@angular/core'
import { CommonModule } from '@angular/common'
import { MainLayoutComponent } from './main-layout/main-layout.component'
import { AuthLayoutComponent } from './auth-layout/auth-layout.component'
import { LibrariesModule } from '../shared/libraries.module'
import { ComponentsModule } from '../components/components.module'

@NgModule({
    declarations:[
        MainLayoutComponent,
        AuthLayoutComponent
    ],
    imports:[
        CommonModule,
        LibrariesModule,
        ComponentsModule,
    ],
    exports:[
        MainLayoutComponent,
        AuthLayoutComponent
    ],
})

export class LayoutsModule { }