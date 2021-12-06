import { ExecutorModel } from "./executor.model"
import { TechnologyModel } from "./technology.model";

export class GalleryModel {
  viewKey: number;
  viewName: string;
  image: string;
  executors: Array<ExecutorModel>;
  tags: Array<string>;
  shortDescription: Array<string>;
  orderNumber?: number;  
}
