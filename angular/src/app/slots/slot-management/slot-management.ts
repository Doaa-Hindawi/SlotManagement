import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SlotService, GenerateSlotsInput, SlotDto } from '../slot';

@Component({
  selector: 'app-slot-management',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './slot-management.html',
  styleUrls: ['./slot-management.scss']
})
export class SlotManagementComponent implements OnInit {

  input: GenerateSlotsInput = {
    startDate: '',
    endDate: '',
    timeZone: 'Africa/Cairo',
    slotDuration: 30
  };

  timeZones = ['Africa/Cairo', 'America/New_York', 'Europe/London', 'Asia/Tokyo'];

  resultMessage = '';
  nextSlots: SlotDto[] = [];
  selectedTimeZone = 'Africa/Cairo';
  isLoading = false;

  constructor(private slotService: SlotService) {}

  ngOnInit() {
    this.loadNextSlots();
  }

  async generateSlots() {
    this.isLoading = true;
    this.resultMessage = '';

    if (new Date(this.input.startDate) > new Date(this.input.endDate)) {
      this.resultMessage = 'Start date must be before or equal to end date';
      this.isLoading = false;
      return;
    }

    try {
      const result = await this.slotService.generate(this.input).toPromise();
      this.resultMessage = `${result?.totalSlotsCreated} slots created successfully!`;
      this.loadNextSlots();
    } catch (err: any) {
      this.resultMessage = 'Fail: ' + (err.error?.error?.message || err.message);
    } finally {
      this.isLoading = false;
    }
  }

  async loadNextSlots() {
    try {
      this.nextSlots = await this.slotService.getNextAvailable(this.selectedTimeZone, 20).toPromise() || [];
    } catch (err) {
      console.error(err);
    }
  }
}