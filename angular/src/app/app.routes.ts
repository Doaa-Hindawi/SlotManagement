import { authGuard, permissionGuard } from '@abp/ng.core';
import { Routes } from '@angular/router';
import { SlotManagementComponent } from './slots/slot-management/slot-management';

export const APP_ROUTES: Routes = [
  { path: '', component: SlotManagementComponent },   
  { path: 'slots', component: SlotManagementComponent }
];
