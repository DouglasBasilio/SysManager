import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
    selector: 'app-category',
    templateUrl: './category.component.html'
})

export class CategoryComponent implements OnInit {

    returnUrl: string = ''
    constructor(private route: ActivatedRoute,
                private router: Router,
    ) { }

    ngOnInit() {

    }
}