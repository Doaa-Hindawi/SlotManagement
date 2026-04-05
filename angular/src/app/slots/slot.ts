import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface GenerateSlotsInput {
  startDate: string;
  endDate: string;
  timeZone: string;
  slotDuration: number;
}

export interface GenerateSlotsResultDto {
  totalSlotsCreated: number;
}

export interface SlotDto {
  localStartTime: string;
  localEndTime: string;
  timeZone: string;
  isBookable: boolean;
}

@Injectable({
  providedIn: 'root'
})
export class SlotService {
  private apiBase = '/api/app/slots';

  constructor(private http: HttpClient) { }

  generate(input: GenerateSlotsInput): Observable<GenerateSlotsResultDto> {
    return this.http.post<GenerateSlotsResultDto>(`${this.apiBase}/generate`, input);
  }

  getNextAvailable(timeZone: string, count: number = 20): Observable<SlotDto[]> {
    return this.http.get<SlotDto[]>(`${this.apiBase}/next-available`, {
      params: { timeZone, count: count.toString() }
    });
  }
}