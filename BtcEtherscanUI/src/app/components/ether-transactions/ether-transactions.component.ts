import { Component, OnInit, ViewChild } from '@angular/core';
import { FilterOptions } from 'src/app/models/filter-options';
import { Txn } from 'src/app/models/txn';
import { Table } from 'primeng/table';
import { EtherTxnService } from 'src/app/services/ethertxn.service';

@Component({
  selector: 'app-ether-transactions',
  templateUrl: './ether-transactions.component.html',
  styleUrls: ['./ether-transactions.component.scss']
})
export class EtherTransactionsComponent implements OnInit {

  txnCols: any[];
  txns: Txn[];
  loading = false;
  totalTxns = 0;

  @ViewChild('dt') table: Table;

  constructor(private ethTxnService: EtherTxnService) { }

  ngOnInit() {
    this.initializeTxnTable();
  }

  public search(options: FilterOptions): void {
    console.log('search clicked');
    this.loading = true;
    this.txns = [];

    this.ethTxnService.getEtherTxns(options).subscribe(response => {
      if (response) {
        this.txns = response;
        this.totalTxns = this.txns.length;
      }
      this.loading = false;
    });
  }

  private initializeTxnTable(): void {
    this.txnCols = [
      { field: 'transactionHash', header: 'Txn Hash' },
      { field: 'blockNumber', header: 'Block' },
      { field: 'from', header: 'From' },
      { field: 'to', header: 'To' },
      { field: 'gas', header: 'Gas' },
      { field: 'gasPrice', header: 'Gas Price' },
      { field: 'value', header: 'Value' },
      { field: 'nonce', header: 'Nonce' },
    ];
  }

}
