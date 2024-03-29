import { CommonModule } from '@angular/common';
import { NgModule } from "@angular/core";
import { MenuComponent } from './menu/menu.component';
import { FooterComponent } from './footer/footer.component';
import { RouterModule } from '@angular/router';

@NgModule({
    declarations: [
        MenuComponent,
        FooterComponent
    ],
    imports: [
        CommonModule,
        RouterModule
    ],
    exports: [
        MenuComponent,
        FooterComponent
    ]
})
export class LayoutModule { }