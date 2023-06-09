import { Injectable } from '@angular/core';
import { MatPaginatorIntl } from '@angular/material/paginator';

@Injectable()
export class PaginatorIntlService extends MatPaginatorIntl {
  itemsPerPageLabel = 'Elementos por página';
  nextPageLabel = 'Siguiente';
  previousPageLabel = 'Anterior';
  firstPageLabel = 'Primera página';
  lastPageLabel = 'Última página';

  getRangeLabel = (page: number, pageSize: number, length: number) => {
    const start = page * pageSize + 1;
    const end = (page + 1) * pageSize;
    return `${start} - ${end} de ${length}`;
  };
}