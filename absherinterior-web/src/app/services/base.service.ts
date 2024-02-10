import { DatePipe } from "@angular/common";

export class BaseService {
  constructor(private datepipe: DatePipe) {}
  
  toQueryString(obj, prefix?) {
    let str = [],
      k,
      v;
    for (const p in obj) {
      if (!obj.hasOwnProperty(p)) {
        continue;
      } // skip things from the prototype
      if (~p.indexOf("[")) {
        k = prefix ? prefix + "." + p : p;
        // only put whatever is before the bracket into new brackets; append the rest
      } else {
        k = prefix ? prefix + "." + p : p;
      }
      v = obj[p];
      if (v instanceof Date) v = this.datepipe.transform(v, "yyyy-MM-dd ");
      if (typeof v != "string" && v && v.length > 0) {
        v.forEach((el) => {
          str.push(
            typeof el == "object"
              ? this.toQueryString(el, k)
              : k + "=" + encodeURIComponent(el)
          );
        });
      } else {
        str.push(
          typeof v == "object"
            ? this.toQueryString(v, k)
            : k + "=" + encodeURIComponent(v)
        );
      }
    }
    return str.join("&");
  }
}
