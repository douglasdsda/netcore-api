export interface Pagination {
    totalItems: number;
   itemsPerPage: number;
   currentPage: number;
   totalPages: number;
}

export class PaginationResult<T> {
 result: T;
 pagination: Pagination;
}
