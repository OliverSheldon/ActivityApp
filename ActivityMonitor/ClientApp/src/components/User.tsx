export default class User
{
    constructor(name: string){
        this.Name = name;
    }

    public Name?: string = undefined;
    private IsOnline?: boolean = undefined;
}