export class GetEventRequest
{
    constructor()
    {
        this.page = 0;
        this.pageSize = 100;
        this.filter = new Filter();
        this.sortBy = new SortBy();
    }

    page: number;
    pageSize: number;    
    filter: Filter;
    sortBy: SortBy;
}

export class SortBy
{
    constructor()
    {
        this.sortType = 1; // date
        this.sortOrder = 2; // desc 
    }

    sortType: number;
    sortOrder: number;
}

export class Filter
{
    constructor()
    {
        this.dateFilter = null;
        this.statusFilter = null;
        this.categoryFilter = null;
    }

    dateFilter: DateFilter | null;
    statusFilter: StatusFilter | null;
    categoryFilter: CategoryFilter | null;
}

export class DateFilter
{
    constructor()
    {
        this.from = "1900-12-13";
        this.to = "2919-12-13";
    }

    from: string | null;
    to: string | null;
}

export class StatusFilter
{

    constructor()
    {
        this.isClosed = false;
    }

    isClosed : boolean
}

export class CategoryFilter
{
    constructor()
    {
        this.categoryIds = Array<number>();
    }
    categoryIds : Array<number>;
}