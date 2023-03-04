import { Theme } from './symbols';

export const darkTheme: Theme = {
  name: 'dark',
  properties: {
    '--body-background': '#1b1b1b',
    '--on-background': '#fff',
    '--main-background': '#292929',
    '--main-font-color': '#dadada',
    '--secondary-background': '#3c3c3c',
    '--secondary-font-color': '#8e8e8e',
    '--font-color': '#dadada',
    '--gradient-default':
      'linear-gradient(135deg, rgba(186,186,186,1) 0%, rgba(98,98,98,1) 100%)',
    '--gradient-primary':
      'linear-gradient(135deg, rgba(69,6,44,1) 0%, rgba(184,36,125,1) 100%)',
    '--gradient-secondary':
      'linear-gradient(135deg, rgba(35,6,52,1) 0%, rgba(115,15,176,1) 100%)',
    '--gradient-info':
      'linear-gradient(135deg, rgba(33,54,96,1) 0%, rgba(71,183,201,1) 100%)',
    '--gradient-success':
      'linear-gradient(135deg, rgba(13,57,60,1) 0%, rgba(55,205,135,1) 100%)',
    '--gradient-danger':
      'linear-gradient(135deg, rgba(73,20,5,1) 0%, rgba(208,37,69,1) 100%)',
    '--gradient-warning':
      'linear-gradient(135deg, rgba(126,47,10,1) 0%, rgba(255,153,36,1) 100%)',
  },
};
