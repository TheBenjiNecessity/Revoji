import { ModelObject } from "./model-object.model";
import { AppUser } from "./appuser.model";
import { Review } from "./review.model";

export class ReviewLike extends ModelObject 
{
    appUserID: number;
    ReviewID: number;
    type: string;
    created: Date;
    
    appUser: AppUser;
    review: Review;

    constructor(ID: number)
    {
        super(ID);
    }
}