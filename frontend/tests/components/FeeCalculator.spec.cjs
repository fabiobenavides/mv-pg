// FeeCalculator.spec.cjs
const { shallowMount } = require('@vue/test-utils');

// Mock the API module
jest.mock('../../src/services/auctionApi', () => ({
  calculateFees: jest.fn()
}));

const FeeCalculator = require('../../src/components/FeeCalculator.vue').default;

const { calculateFees } = require('../../src/services/auctionApi');

describe('FeeCalculator.vue', () => {
  beforeEach(() => {
    jest.clearAllMocks();
  });

  it('shows an error if trying to calculate without valid data', async () => {
    const wrapper = shallowMount(FeeCalculator);
    await wrapper.vm.onCalculate();
    expect(wrapper.vm.error).toContain('Please enter a valid total price');
  });

  it('calls calculateFees correctly and updates the results', async () => {
    calculateFees.mockResolvedValueOnce({
      buyerFee: 100,
      sellerFee: 50,
      associationFee: 25,
      storageFee: 10,
      totalFees: 185
    });

    const wrapper = shallowMount(FeeCalculator);
    await wrapper.setData({ basePrice: 1000, vehicleType: 'common' });
    await wrapper.vm.onCalculate();

    expect(calculateFees).toHaveBeenCalledWith(1000, 'common');
    expect(wrapper.vm.result.totalFees).toBe(185);
    expect(wrapper.vm.result.total).toBe(1185);
    expect(wrapper.vm.error).toBeNull();
  });

  it('handles API errors correctly', async () => {
    calculateFees.mockRejectedValueOnce(new Error('API failed'));

    const wrapper = shallowMount(FeeCalculator);
    await wrapper.setData({ basePrice: 500, vehicleType: 'luxury' });
    await wrapper.vm.onCalculate();

    expect(wrapper.vm.error).toBe('API failed');
    expect(wrapper.vm.result).toEqual({
      buyerFee: 0,
      sellerFee: 0,
      associationFee: 0,
      storageFee: 0,
      totalFees: 0,
      total: 500
    });
  });

  it('formats currency correctly', () => {
    const wrapper = shallowMount(FeeCalculator);
    const formatted = wrapper.vm.formatCurrency(1234.56);
    expect(formatted).toBe('$1,234.56');
  });
});
