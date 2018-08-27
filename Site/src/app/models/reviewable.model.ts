import { ModelObject } from './model-object.model'
import { ReviewableContent } from './reviewable-content.model';
import { ReviewableInfo } from './reviewable-info.model';

export class Reviewable extends ModelObject {
    title: string;
    type: string;
    description: string;
    titleImageUrl: string;

    content: ReviewableContent;
    info: ReviewableInfo;
}