export interface PagedResult<T> {
    page: number,
    pageSize: number,
    totalCount: number,
    totalPages: number,
    data: T
}

export interface PagedResultWithTotals<T> extends PagedResult<T> {
    totalIncome: number,
    totalExpense: number,
    balance: number
}