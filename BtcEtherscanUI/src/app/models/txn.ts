export interface Txn {
    transactionHash?: string;
    transactionIndex?: string;
    blockHash?: string;
    blockNumber?: string;
    from?: string;
    to?: string;
    gas?: number;
    gasPrice?: number;
    value?: number;
    input?: string;
    nonce?: number;
    r?: string;
    s?: string;
    v?: string;
}
