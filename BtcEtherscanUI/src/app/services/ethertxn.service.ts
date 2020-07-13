import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FilterOptions } from '../models/filter-options';
import { Observable, of } from 'rxjs';
import { take, map, catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { isNullOrUndefined } from 'util';
import { Txn } from '../models/txn';

@Injectable({
  providedIn: 'root'
})
export class EtherTxnService {
  private readonly baseUrl = environment.base_api_url;

  constructor(private http: HttpClient) { }

  getEtherTxns(options: FilterOptions): Observable<any> {

    const url = `${this.baseUrl}/txns/${options.blockNumber}`;

    return this.http.get<Txn[]>(url).pipe(
      map((response: any) => response),
      catchError(() => {
        console.log('Unable to get ethereum transactions. Error occurred while trying to retrieve transactions');
        return of(null);
      })
    );
  }
}
