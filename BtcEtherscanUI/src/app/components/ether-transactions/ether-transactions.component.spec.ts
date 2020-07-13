import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EtherTransactionsComponent } from './ether-transactions.component';

describe('EtherTransactionsComponent', () => {
  let component: EtherTransactionsComponent;
  let fixture: ComponentFixture<EtherTransactionsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EtherTransactionsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EtherTransactionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
