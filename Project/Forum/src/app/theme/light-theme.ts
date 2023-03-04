import { Theme } from './symbols';

export const lightTheme: Theme = {
  name: 'light',
  properties: {
    '--body-background': 'rgb(222, 222, 222)',
    '--on-background': '#000',
    '--main-background': '#ffffff',
    '--main-font-color': '#1d1d1d',
    '--secondary-background': '#cecece',
    '--secondary-font-color': '#8e8e8e',
    '--font-color': '#ffffff',
    '--gradient-default':
      'linear-gradient(135deg, rgba(98,98,98,1) 0%, rgba(61,61,61,1) 100%)',
    '--gradient-primary':
      'linear-gradient(135deg, rgba(255,118,201,1) 0%, rgba(184,36,125,1) 100%)',
    '--gradient-secondary':
      'linear-gradient(135deg, rgba(193,110,241,1) 0%, rgba(115,15,176,1) 100%)',
    '--gradient-info':
      'linear-gradient(135deg, rgba(115,235,255,1) 0%, rgba(47,102,182,1) 100%)',
    '--gradient-success':
      'linear-gradient(135deg, rgba(48,214,201,1) 0%, rgba(55,205,135,1) 100%)',
    '--gradient-danger':
      'linear-gradient(135deg, rgba(241,144,103,1) 0%, rgba(173,22,50,1) 100%)',
    '--gradient-warning':
      'linear-gradient(135deg, rgba(213,171,78,1) 0%, rgba(208,85,37,1) 100%)',
  },
};
