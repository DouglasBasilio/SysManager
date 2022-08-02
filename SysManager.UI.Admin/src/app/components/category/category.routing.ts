import { CategoryMaintenanceComponent } from './category-maintenance/category-maintenance.component';
import { CategoryComponent } from './category.component';
import { CategoryViewComponent } from './models/category-view-component';
import { Routes } from '@angular/router';

export const CategoryRoutes: Routes = [
    {
        path: '',
        component: CategoryViewComponent,
        children: [
            {
                path: 'category',
                component: CategoryComponent,
                data: { name: 'Pesquisar categoria', title: 'Pesquisar Categoria' }
            },
            {
                path: 'maintenance',
                component: CategoryMaintenanceComponent,
                data: { name: 'Inserir categoria', title: 'Inserir Categoria' }
            },
            {
                path: 'maintenance/:id',
                component: CategoryMaintenanceComponent,
                data: { name: 'Alterar categoria', title: 'Alterar Categoria' }
            },
        ]
    },
]