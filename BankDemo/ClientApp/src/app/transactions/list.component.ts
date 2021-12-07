import { AfterViewInit, Component, OnInit, ViewChild } from "@angular/core";
import { MatPaginator } from "@angular/material/paginator";
import { MatSort } from "@angular/material/sort";
import { MatTableDataSource } from "@angular/material/table";
import { Transactions } from "./transactions.model";
import { TransactionService } from "./transactions.service";

@Component({
  selector: 'transactions',
  templateUrl: 'list.component.html'
})

export class TransactionsListComponent implements OnInit, AfterViewInit {
  columns: string[] = ['Transactions_Id', 'Sender', 'Receiver','Credit','Debit', 'Date'];//, 'details-edit-delete'
  dataSource = new MatTableDataSource<Transactions>();

  @ViewChild(MatSort, { static: true }) sort: MatSort;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  constructor(private transactionService: TransactionService) {
    this.dataSource.filterPredicate = (transaction: Transactions, filter) => {
      return transaction.Sender.toLowerCase().includes(filter.toLowerCase()) ||
        transaction.Receiver.toLowerCase().includes(filter.toLowerCase());
    }
  }

  ngOnInit() {
    this.get();
  }

  ngAfterViewInit() {
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
  }

  public get() {
    this.transactionService.get()
      .subscribe(res => {
        this.dataSource.data = res as Transactions[];
      });
  }

  public filter(filter) {
    this.dataSource.filter = filter.trim().toLowerCase();
  }
/*
  delete(Id) {
    if (confirm('Are you sure to delete this record?')) {
      this.transactionService.delete(Id)
        .subscribe(() => {
          this.get();
        },
          err => {
            console.log((err));
          })
    }
  }*/
}
