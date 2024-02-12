import { Component, OnInit, QueryList, ViewChildren } from '@angular/core';
import { Observable } from 'rxjs';
import { SortEvent, Student } from 'src/shared/models/student';
import { TableService } from './table.service';
import {Sort} from '@angular/material/sort';
import { SortableHeaderDirective } from './sort';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.css'],
})
export class TableComponent implements OnInit {

  data$: Observable<Student[]>;
  total$: Observable<number>;
  @ViewChildren(SortableHeaderDirective) headers: QueryList<SortableHeaderDirective>;
  constructor(public service: TableService) {
    this.data$ = service.data$;
    this.total$ = service.total$;
  }

  ngOnInit(): void {
    this.service.refresh();
  }

  onSort({ column, direction }: SortEvent) {
    // resetting other headers
    this.headers.forEach((header) => {
      if (header.sortable !== column) {
        header.direction = '';
      }
    });
  
    // sorting countries
    if (direction === '' || column === '') {
      this.service.sort = '';
    } else {
      this.service.sort = `${column}::${direction}`;
    }
    this.service.refresh();
  }



  


}
