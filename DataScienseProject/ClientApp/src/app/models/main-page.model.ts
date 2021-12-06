import { ExecutorModel } from "./executor.model"
import { LayoutDataModel } from "./layout-data.model"
import { ProjectTypeModel } from "./project-type.model";
import { TechnologyModel } from "./technology.model";

export class MainPageModel {
  projectTypeModels: Array<ProjectTypeModel>;
  executorModels: Array<ExecutorModel>;
  tehnologyModels: Array<TechnologyModel>;
  layoutDataModels: Array<LayoutDataModel>;
}
