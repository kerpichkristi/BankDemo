 import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class TransactionService {
  baseUrl: string;
  apiUrl = 'api/Transactions/';


  constructor(private formBuilder: FormBuilder,
    private httpClient: HttpClient,
    @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

 public get(Transactions_Id?) {
    if (Transactions_Id) {
      return this.httpClient.get(this.baseUrl + this.apiUrl + Transactions_Id);
    }
    else {
      return this.httpClient.get(this.baseUrl + this.apiUrl);
    }
  }

  delete(Transactions_Id) {
    return this.httpClient.delete(this.baseUrl + this.apiUrl + Transactions_Id);
  }

  put(Transactions) {
    return this.httpClient.put(this.baseUrl + this.apiUrl + Transactions.Transactions_Id, Transactions);
  }
}
