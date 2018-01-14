import { Component, ViewEncapsulation } from '@angular/core';

@Component({
    selector: 'relicrow',
    templateUrl: './relicrow.component.html',
    styleUrls: [
        './relicrow.component.css'
    ]
})

export class RelicRowComponent {
    name: string;
    pPrice: number;
    dPrice: number;
    drops: any[];

    constructor() {
        this.name = "Axi V1";
        this.pPrice = 10000;
        this.dPrice = 10000;
        this.drops = [];
    }
}