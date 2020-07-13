import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { FilterOptions } from 'src/app/models/filter-options';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss']
})
export class SearchComponent implements OnInit {
  private searchString: string;

  @Output() searchOnEnter = new EventEmitter<FilterOptions>();

  constructor() { }

  ngOnInit() {
  }

  get filter(): string {
    return this.searchString;
  }

  set filter(filter: string) {
    this.searchString = filter;
    const searchParameter: FilterOptions = {
      blockNumber: this.searchString,
    };
  }

  onSearch(): void {
    this.searchOnEnter.emit({
      blockNumber: this.searchString,
    });
  }
}
