import { NbMenuItem } from "@nebular/theme";
import { title } from "process";

export const MENU_ITEMS: NbMenuItem[] = [
  {
    title: "Dashboard",
    link: "/pages/dashboard",
    home: true,
    icon: {
      icon: "fa-chart-line",
      pack: "font-awesome",
    },
    children: [
      {
        title: "Network Dashboard",
        icon: {
          icon: "fa-map-marked-*",
          pack: "font-awesome",
        },
        children: [
          {
            title: "Performance Category 1",
            icon: {
              icon: "fa-map-marked-alt*",
              pack: "font-awesome",
            },
          },
          {
            title: "Performance Category 2",
            icon: {
              icon: "fa-road*",
              pack: "font-awesome",
            },
          },
          {
            title: "Performance Category 3",
            icon: {
              icon: "fa-clone*",
              pack: "font-awesome",
            },
          },
          {
            title: "Performance Category 4",
            icon: {
              icon: "fa-clone*",
              pack: "font-awesome",
            },
          },
          {
            title: "Performance Category 5",
            icon: {
              icon: "fa-clone*",
              pack: "font-awesome",
            },
          },
        ],
      },
      {
        title: "Marketing Dashboards",
        icon: {
          icon: "fa-road*",
          pack: "font-awesome",
        },
        children: [
          {
            title: "Customer Score",
            icon: {
              icon: "fa-map-marked-alt*",
              pack: "font-awesome",
            },
          },
          {
            title: "Revenue Assurance",
            icon: {
              icon: "fa-road*",
              pack: "font-awesome",
            },
          },
          {
            title: "Engagement",
            icon: {
              icon: "fa-clone*",
              pack: "font-awesome",
            },
          },
        ],
      },
      {
        title: "Site Dashboard",
        icon: {
          icon: "fa-clone*",
          pack: "font-awesome",
        },
        children: [
          {
            title: "Cost",
            icon: {
              icon: "fa-map-marked-alt*",
              pack: "font-awesome",
            },
          },
          {
            title: "Assets",
            icon: {
              icon: "fa-road*",
              pack: "font-awesome",
            },
          },
        ],
      },
      {
        title: "Alarms/Notifications",
        icon: {
          icon: "fa-clone*",
          pack: "font-awesome",
        },
      },
    ],
  },

  {
    title: "Data Management",
    link: "/pages/DataManagement",
    home: true,
    icon: {
      icon: "fa-server",
      pack: "font-awesome",
    },
    children: [
      {
        title: "World Region",
        icon: {
          icon: "fa-globe-africa",
          pack: "font-awesome",
        },
        link: "/pages/world-region",
      },
      {
        title: "Country",
        icon: {
          icon: "fa-flag",
          pack: "font-awesome",
        },
        link: "/pages/country",
      },
      {
        title: "States/Regions",
        icon: {
          icon: "fa-map-signs",
          pack: "font-awesome",
        },
        link: "/pages/state-region",
      },
      {
        title: "Cities",
        icon: {
          icon: "fa-building",
          pack: "font-awesome",
        },
        link: "/pages/city",
      },
      {
        title: "Districts",
        icon: {
          icon: "fa-map-marker",
          pack: "font-awesome",
        },
        link: "/pages/district",
      },
      {
        title: "Cluster",
        icon: {
          icon: "fa-toolbox",
          pack: "font-awesome",
        },
        link: "/pages/cluster",
      },

      {
        title: "Operator Groups",
        icon: {
          icon: "fa-road*",
          pack: "font-awesome",
        },
        children: [
          {
            title: "Operator Groups",
            icon: {
              icon: "fa-map-marked-al*t",
              pack: "font-awesome",
            },
          },
          {
            title: "Operator",
            icon: {
              icon: "fa-road*",
              pack: "font-awesome",
            },
          },
        ],
      },
      {
        title: "Role Management",
        icon: {
          icon: "fa-map-marked-alt*",
          pack: "font-awesome",
        },
      },
      {
        title: "Users Management",
        icon: {
          icon: "fa-map-marked-alt*",
          pack: "font-awesome",
        },
      },
      {
        title: "Network Configurations",
        icon: {
          icon: "fa-clone",
          pack: "font-awesome",
        },
        children: [
          {
            title: "Access",
            icon: {
              icon: "fa-map-marked-alt",
              pack: "font-awesome",
            },
            children: [
              {
                title: "Access Technology",
                icon: {
                  icon: "fa-microchip",
                  pack: "font-awesome",
                },
                link: "/pages/access-technology",
              },
              {
                title: "Vendor",
                icon: {
                  icon: "fa-store",
                  pack: "font-awesome",
                },
                link: "/pages/vendor",
              },
              {
                title: "Goal",
                icon: {
                  icon: "fa-bullseye",
                  pack: "font-awesome",
                },
                link: "/pages/goal",
              },
              {
                title: "Core-Type",
                icon: {
                  icon: "fa-ruler-horizontal",
                  pack: "font-awesome",
                },
                link: "/pages/core-type",
              },
              {
                title: "BSC",
                icon: {
                  icon: "fa-road*",
                  pack: "font-awesome",
                },
              },
              {
                title: "RNC",
                icon: {
                  icon: "fa-road*",
                  pack: "font-awesome",
                },
              },
              {
                title: "Site",
                icon: {
                  icon: "fa-road*",
                  pack: "font-awesome",
                },
              },
              {
                title: "Cell (Google Map capability(Lat+Long))",
                icon: {
                  icon: "fa-map*",
                  pack: "font-awesome",
                },
                link: "/pages/core-type",
              },
            ],
          },
        ],
      },

      {
        title: "Performance Category 1 Categories",
        icon: {
          icon: "fa-road*",
          pack: "font-awesome",
        },
        children: [
          {
            title: "Access KPI Types",
            icon: {
              icon: "fa-road*",
              pack: "font-awesome",
            },
          },
          {
            title: "Site KPI Types",
            icon: {
              icon: "fa-road*",
              pack: "font-awesome",
            },
          },
          {
            title: "Network KPI Types",
            icon: {
              icon: "fa-road*",
              pack: "font-awesome",
            },
          },
        ],
      },
      {
        title: "Performance Category 2 Categories",
        icon: {
          icon: "fa-road*",
          pack: "font-awesome",
        },
        children: [
          {
            title: "Core KPI Types",
            icon: {
              icon: "fa-road*",
              pack: "font-awesome",
            },
          },
        ],
      },
      {
        title: "Performance Category 3 Categories",
        icon: {
          icon: "fa-road*",
          pack: "font-awesome",
        },
      },
      {
        title: "Performance Category 4 Categories",
        icon: {
          icon: "fa-road*",
          pack: "font-awesome",
        },
      },
      {
        title: "Performance Category 5 Categories",
        icon: {
          icon: "fa-road*",
          pack: "font-awesome",
        },
      },
      {
        title: "Feeds Categories",
        icon: {
          icon: "fa-road*",
          pack: "font-awesome",
        },
      },
      {
        title: "Site Management Categories",
        icon: {
          icon: "fa-road*",
          pack: "font-awesome",
        },
      },
      {
        title: "Customer Marketing Categories",
        icon: {
          icon: "fa-road*",
          pack: "font-awesome",
        },
      },
      {
        title: "Engagement Categories",
        icon: {
          icon: "fa-road*",
          pack: "font-awesome",
        },
      },
      {
        title: "Other System Categoriess",
        icon: {
          icon: "fa-road*",
          pack: "font-awesome",
        },
      },
      {
        title: "Revenues Categories",
        icon: {
          icon: "fa-road*",
          pack: "font-awesome",
        },
      },
    ],
  },
];
