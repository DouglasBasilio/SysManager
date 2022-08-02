import { ProductTypeMaintenanceComponent } from './product-type-maintenance/product-type-maintenance.component';
import { ProductTypeComponent } from './product-type.component';
import { ProductTypeViewComponent } from './models/product-type-view-component';
import { Routes } from '@angular/router';

export const ProductTypeRoutes: Routes = [
    {
        path: '',
        component: ProductTypeViewComponent,
        children: [
            {
                path: 'product-type',
                component: ProductTypeComponent,
                data: { name: 'Pesquisar tipo de produto', title: 'Pesquisar Tipo de Produto' }
            },
            {
                path: 'maintenance',
                component: ProductTypeMaintenanceComponent,
                data: { name: 'Inserir tipo de produto', title: 'Inserir Tipo de Produto' }
            },
            {
                path: 'maintenance/:id',
                component: ProductTypeMaintenanceComponent,
                data: { name: 'Alterar tipo de produto', title: 'Alterar Tipo de Produto' }
            },
        ]
    },
]