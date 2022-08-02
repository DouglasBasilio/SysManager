import { UnityMaintenanceComponent } from './unity-maintenance/unity-maintenance.component';
import { UnityComponent } from './unity.component';
import { UnityViewComponent } from './models/unity-view-component';
import { Routes } from '@angular/router';

export const UnityRoutes: Routes = [
    {
        path: '',
        component: UnityViewComponent,
        children: [
            {
                path: 'unity',
                component: UnityComponent,
                data: { name: 'Pesquisar unidade de medida', title: 'Pesquisar Unidade de Medida' }
            },
            {
                path: 'maintenance',
                component: UnityMaintenanceComponent,
                data: { name: 'Inserir unidade de medida', title: 'Inserir Unidade de Medida' }
            },
            {
                path: 'maintenance/:id',
                component: UnityMaintenanceComponent,
                data: { name: 'Alterar unidade de medida', title: 'Alterar Unidade de Medida' }
            },
        ]
    },
]