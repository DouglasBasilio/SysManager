export class CategoryFilter {
    name: string;
    active: string;
    page: number;
    pageSize: number;

    constructor(_name: string, _active: string, _page: number, _pageSize: number){
        this.name = _name;
        this.active = _active;
        this.page = _page;
        this.pageSize = _pageSize;
    }
}