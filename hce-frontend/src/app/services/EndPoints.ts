import { environment } from "../../environments/environment";

export const EndPoints = {
  baseUrl: environment.baseApiUrl,
  Lookups: {
    worldRegions: "admin/Lookups/GetAllWorldRegions",
    countries: "admin/Lookups/GetAllCountries",
    stateRegions: "admin/Lookups/GetAllStateRegions",
    cities: "admin/Lookups/GetAllCities",
    distrcits: "admin/Lookups/GetAllDistricts",
  },
  AccessTechnology: {
    list: "admin/accessTechnolgy/Search",
    get: "admin/accessTechnolgy/Get",
    create: "admin/accessTechnolgy/AddAccessTechnolgy",
    update: "admin/accessTechnolgy/UpdateAccessTechnology",
    delete: "admin/accessTechnolgy/Delete",
  },
  Goal: {
    list: "admin/goal/Search",
    get: "admin/goal/Get",
    create: "admin/goal/AddGoal",
    update: "admin/goal/UpdateGoal",
    delete: "admin/goal/Delete",
  },
  Vendor: {
    list: "admin/vendor/Search",
    get: "admin/vendor/Get",
    create: "admin/vendor/AddVendor",
    update: "admin/vendor/UpdateVendor",
    delete: "admin/vendor/Delete",
  },
  CoreType: {
    list: "admin/CoreType/Search",
    get: "admin/CoreType/Get",
    create: "admin/CoreType/AddCoreType",
    update: "admin/CoreType/UpdateCoreType",
    delete: "admin/CoreType/Delete",
  },
  WorldRegion: {
    list: "admin/WorldRegion/Search",
    get: "admin/WorldRegion/Get",
    create: "admin/WorldRegion/AddWorldRegion",
    update: "admin/WorldRegion/UpdateWorldRegion",
    delete: "admin/WorldRegion/Delete",
  },
  StateRegion: {
    list: "admin/StateRegion/Search",
    get: "admin/StateRegion/Get",
    getByCountryId: "admin/StateRegion/GetStateRegionsByCountryId",
    create: "admin/StateRegion/AddRegion",
    update: "admin/StateRegion/UpdateRegion",
    delete: "admin/StateRegion/Delete",
  },
  Country: {
    list: "admin/Country/Search",
    get: "admin/Country/Get",
    create: "admin/Country/AddCountry",
    update: "admin/Country/UpdateCountry",
    delete: "admin/Country/Delete",
  },
  City: {
    list: "admin/City/Search",
    get: "admin/City/Get",
    create: "admin/City/AddCity",
    update: "admin/City/UpdateCity",
    delete: "admin/City/Delete",
    getByStateRegionId: "admin/City/GetCitiesByStateRegionId",
  },
  District: {
    list: "admin/District/Search",
    get: "admin/District/Get",
    create: "admin/District/AddDistrict",
    update: "admin/District/UpdateDistrict",
    delete: "admin/District/Delete",
    getByCityId: "admin/District/GetDistrictsByCityId",
  },
  Cluster: {
    list: "admin/Cluster/Search",
    get: "admin/Cluster/Get",
    create: "admin/Cluster/AddCluster",
    update: "admin/Cluster/UpdateCluster",
    delete: "admin/Cluster/Delete",
    getByDistrictId: "admin/Cluster/GetClustersByDistrictId",
  },
};