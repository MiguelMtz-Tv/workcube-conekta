import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core'
import { MatButtonModule } from '@angular/material/button'
import { TextFieldModule } from '@angular/cdk/text-field'
import { MatInputModule } from '@angular/material/input'
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'
import {MatTabsModule} from '@angular/material/tabs'; 
import {MatMenuModule} from '@angular/material/menu';
import {MatExpansionModule} from '@angular/material/expansion'; 
import {MatCardModule} from '@angular/material/card'; 
import {MatRadioModule, MAT_RADIO_DEFAULT_OPTIONS} from '@angular/material/radio'; 
import {MatDialogModule} from '@angular/material/dialog'; 
import {MatCheckboxModule} from '@angular/material/checkbox'; 
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import {MatRippleModule} from '@angular/material/core';
import { NgxSpinnerModule } from "ngx-spinner";
import {MatPaginatorModule} from '@angular/material/paginator'; 
import {MatTableModule} from '@angular/material/table'; 
import {MatIconModule} from '@angular/material/icon'; 

@NgModule({
    imports:[
        MatButtonModule,
        TextFieldModule,
        MatInputModule,
        BrowserAnimationsModule,
        MatTabsModule,
        MatMenuModule,
        MatExpansionModule,
        MatRadioModule,
        MatCardModule,
        MatDialogModule,
        MatCheckboxModule,
        MatProgressSpinnerModule,
        MatRippleModule,
        NgxSpinnerModule,
        MatPaginatorModule,
        MatTableModule,
        MatIconModule,
    ],
    exports:[
        MatButtonModule,
        TextFieldModule,
        MatInputModule,
        BrowserAnimationsModule,
        MatTabsModule,
        MatMenuModule,
        MatExpansionModule,
        MatRadioModule,
        MatCardModule,
        MatDialogModule,
        MatCheckboxModule,
        MatProgressSpinnerModule,
        MatRippleModule,
        NgxSpinnerModule,
        MatPaginatorModule,
        MatTableModule,
        MatIconModule,
    ],
    providers: [{
        provide: MAT_RADIO_DEFAULT_OPTIONS,
        useValue: { color: 'primary' },
    }],

    schemas: [CUSTOM_ELEMENTS_SCHEMA],
})

export class MaterialModule { }