import { ExecutorModel } from "./executor.model"
import { TechnologyModel } from "./technology.model";

export class GalleryModel {
  viewKey: number;
  viewName: string;
  image: string;
  executors: Array<ExecutorModel>;
  tags: Array<TechnologyModel>;
  shortDescription: string;
  orderNumber: number;  
}
