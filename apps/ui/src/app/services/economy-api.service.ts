import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { appSettings } from '../app.settings';
import {
  AdminUserSummary,
  DashboardResponse,
  GrantCogsPayload,
  GrantGearPayload,
  InventoryItem,
  PurchaseReceipt,
  StoreItem,
  UpsertGearPayload
} from '../models/economy.models';

@Injectable({
  providedIn: 'root'
})
export class EconomyApiService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = appSettings.apiBaseUrl;

  startGoogleLogin(): void {
    window.location.assign(`${this.baseUrl}/api/auth/login`);
  }

  logout(): Observable<{ message: string }> {
    return this.http.post<{ message: string }>(`${this.baseUrl}/api/auth/logout`, {}, {
      withCredentials: true
    });
  }

  getDashboard(): Observable<DashboardResponse> {
    return this.http.get<DashboardResponse>(`${this.baseUrl}/api/economy/dashboard`, {
      withCredentials: true
    });
  }

  buyGear(gearItemId: number, quantity: number): Observable<PurchaseReceipt> {
    return this.http.post<PurchaseReceipt>(`${this.baseUrl}/api/economy/store/${gearItemId}/buy`, {
      quantity
    }, {
      withCredentials: true
    });
  }

  getAdminUsers(): Observable<AdminUserSummary[]> {
    return this.http.get<AdminUserSummary[]>(`${this.baseUrl}/api/admin/users`, {
      withCredentials: true
    });
  }

  getAdminGearItems(includeInactive: boolean): Observable<StoreItem[]> {
    return this.http.get<StoreItem[]>(`${this.baseUrl}/api/admin/gear-items`, {
      withCredentials: true,
      params: {
        includeInactive
      }
    });
  }

  grantCogs(payload: GrantCogsPayload): Observable<AdminUserSummary> {
    return this.http.post<AdminUserSummary>(`${this.baseUrl}/api/admin/grant-cogs`, payload, {
      withCredentials: true
    });
  }

  grantGear(payload: GrantGearPayload): Observable<InventoryItem> {
    return this.http.post<InventoryItem>(`${this.baseUrl}/api/admin/grant-gear`, payload, {
      withCredentials: true
    });
  }

  createGearItem(payload: UpsertGearPayload): Observable<StoreItem> {
    return this.http.post<StoreItem>(`${this.baseUrl}/api/admin/gear-items`, payload, {
      withCredentials: true
    });
  }

  updateGearItem(gearItemId: number, payload: UpsertGearPayload): Observable<StoreItem> {
    return this.http.put<StoreItem>(`${this.baseUrl}/api/admin/gear-items/${gearItemId}`, payload, {
      withCredentials: true
    });
  }
}
