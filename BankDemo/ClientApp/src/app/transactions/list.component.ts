import { AfterViewInit, Component, OnInit, ViewChild } from "@angular/core";
import { MatPaginator } from "@angular/material/paginator";
import { MatSort } from "@angular/material/sort";
import { MatSortable, MatTableDataSource } from "@angular/material";
import { Transactions } from "./transactions.model";
import { TransactionService } from "./transactions.service";

@Component({
  selector: 'transactions',
  templateUrl: 'list.component.html'
})

export class TransactionsListComponent implements OnInit, AfterViewInit {
  columns: string[] = ['Transactions_Id', 'Sender', 'Receiver', 'Credit', 'Debit', 'Date', 'Delete'];
  dataSource = new MatTableDataSource<Transactions>();
  
  @ViewChild(MatSort, { static: true }) sort: MatSort;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  constructor(private transactionService: TransactionService) {}
  
  ngOnInit() {
    this.get();
  }

  ngAfterViewInit() {
    setTimeout(() => {
      this.dataSource.sort = this.sort;
      this.dataSource.paginator = this.paginator; });
   
  }

  public get() {
    this.transactionService.get()
      .subscribe(res => {
        this.dataSource.data = res as Transactions[];
      });
  }

  public filter(filter: string) {
  this.dataSource.filter = filter.trim().toLocaleLowerCase();
    
  }

  delete(Transactions_Id) {
    if (confirm('Are you sure to delete this record?')) {
      this.transactionService.delete(Transactions_Id)
        .subscribe(() => {
          this.get();
        },
          err => {
            console.log((err));
          })
    }
  }

}

